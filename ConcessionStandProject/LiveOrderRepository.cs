using Dapper;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ConcessionStandProject
{
    public class DbOrder
    {
        public string OrderId { get; set; }
        public double Total { get; set; }
        public bool IsCompleted { get; set; }   
    }

    public class DbOrderProduct
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Sku { get; set; }
    }

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

            var order = new Order();

            conn.Query<DbOrder, DbOrderProduct, Order>(
                query,
                (o, op) =>
                {
                    order.OrderId = Guid.Parse(o.OrderId);
                    order.Total = o.Total;
                    order.IsCompleted = o.IsCompleted;

                    if (op != null)
                        order.Add(new Product(op.Name, op.Price, op.Sku, ""));
                    
                    return null;
                },
                new { orderid = OrderId },
                splitOn: "Name"
            );
            if (order.IsCompleted)
            {
                order.GenerateReceipt();
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using var conn = new MySqlConnection(connectionStringProvider.GetConnectionString());
            return conn.Query<Order>("SELECT * FROM orderproduct;");
        }

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
