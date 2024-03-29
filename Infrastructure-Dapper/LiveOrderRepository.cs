﻿using Dapper;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using ConcessionStandProject;

namespace Infrastructure.Dapper
{
    public class LiveOrderRepository : IOrderRepository
    {
        private readonly IConnectionStringProvider connectionStringProvider;
        public LiveOrderRepository(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }
        public void CreateOrder(Order order)
        {
            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
        
                conn.Execute("INSERT INTO `order` (OrderId) VALUES (@orderid);",
                new { OrderId = order.OrderId});

        }

        public Order Find(Guid OrderId)
        {
            const string query = @"
SELECT o.OrderId
     , o.Total
     , o.IsCompleted
     , op.Name
     , op.Price
     , op.Sku
  FROM `Order` AS o
LEFT JOIN orderproduct AS op 
ON op.OrderId = o.OrderId
WHERE o.OrderId = @OrderId;";

            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());

            var orders = conn.Query<DbOrder, DbOrderProduct, Order>(
                query,
                (o, op) =>
                {
                    var order = new Order(o.OrderId, o.Total, o.IsCompleted);
                    if (op != null)
                        order.Add(new Product(op.Name, op.Price, op.Sku, ""));
                    
                    return order;
                },
                new { orderid = OrderId },
                splitOn: "Name"
            );

            var products = orders.SelectMany(x => x.Products).ToList();
            var order = orders.FirstOrDefault();
            order.Products.Clear();
            foreach (var product in products)
            {
                order.Add(product);
            }

            if (order.IsCompleted)
            {
                order.GenerateReceipt();
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
                const string query = @"
SELECT o.OrderId
     , o.Total
     , o.IsCompleted
     , op.Name
     , op.Price
     , op.Sku
  FROM `Order` AS o
LEFT JOIN orderproduct AS op 
ON op.OrderId = o.OrderId;";

                using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());

                var orders = new Dictionary<Guid, Order>();

            conn.Query<DbOrder, DbOrderProduct, Order>(
                query,
                (o, op) =>
                {
                    Order order;
                    Guid orderId = Guid.Parse(o.OrderId);
                    if (orders.ContainsKey(orderId))
                    {
                        order = orders[orderId];
                    }
                    else
                    {
                        order = new Order();
                        orders.Add(orderId, order);
                        order.OrderId = Guid.Parse(o.OrderId);
                        order.Total = o.Total;
                        order.IsCompleted = o.IsCompleted;
                    }

                        if (op != null)
                            order.Add(new Product(op.Name, op.Price, op.Sku, ""));

                        return null;
                    },
                    splitOn: "Name"
                );
                return orders.Values;
            }
            //using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
            //return conn.Query("SELECT OrderId, Total, IsCompleted FROM `order`;")
            //    .Select(x => new Order(x.OrderId, x.Total, x.IsCompleted));
        

        public void Update(Order order)
        {
            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
            conn.Open();
            using var transaction = conn.BeginTransaction(); // creates transaction
 
            try
            {
                // do all ur inserts here
                var oldOrder = Find(order.OrderId);

                var orderProductCount = new Dictionary<int, int>();
                foreach (var product in order.Products)
                    orderProductCount.TryAdd(product.Sku, order.Products.Count(x => x.Sku == product.Sku));

                conn.Execute("UPDATE `order` SET Total = @total, IsCompleted = @iscompleted WHERE OrderId = @orderid ;",
                   new { total = order.Total, iscompleted = order.IsCompleted, orderid = order.OrderId} );

                foreach (var (sku, count) in orderProductCount)
                {
                    if (count > oldOrder.Products.Count(x => x.Sku == sku))
                    {
                        var product = order.Products.First(x => x.Sku == sku);
                        conn.Execute("INSERT INTO orderproduct (Name, Price, Sku, OrderId) VALUES (@name, @price, @sku, @orderid);",
                        new { name = product.Name, price = product.Price, sku = product.Sku, OrderId = order.OrderId });
                    }
                }

                transaction.Commit(); // completes the transaction and commits everything to the database.
            }
            catch (Exception ex)
            {
                // something went wrong
                transaction.Rollback();
            }
            conn.Close();
          
        }
    }
}
