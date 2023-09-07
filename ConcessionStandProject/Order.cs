using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcessionStandProject
{
    public class Order
    {
        public Order()  // there are no parameters in my constructor = Fact Test 
        {
            Products = new List<Product>();  //creates a new list of Products when a new Order is created.
            OrderId = Guid.NewGuid();    //creates new guid ID for each new order. 
        }

        public Order(string orderId, decimal total, bool isCompleted)
        {
            OrderId = Guid.Parse(orderId);
            Total = total;
            IsCompleted = isCompleted;
            Products = new List<Product>(); 
        }

        public List<Product> Products { get; set; }
        public Guid OrderId { get; set; }
        public decimal Subtotal { get; set; }
        public Receipt Receipt { get; private set; }
        public decimal Total { get; set; }
        public bool IsCompleted { get; set; }

        public void Add(Product product)
        {
            Products.Add(product);
            CalculateSubtotal();
        }

        public void Submit()
        {
            if(Products.Count <= 0)
            {
                throw new InvalidOperationException("Can't submit order without products");
            }

            GenerateReceipt();          
            IsCompleted = true;

        }

        public void CalculateSubtotal()
        {
            var subtotal = Products.Sum(product => product.Price);

            Subtotal = subtotal;
        }

        public void GenerateReceipt()
        {
            Receipt = new Receipt(Products, OrderId);
            var total = Subtotal;
            Total = total;

        }

        public void RemoveProduct(int sku)
        { 
            _ = Products.Where(p => p.Sku == sku);
            foreach (Product p in Products)
            {
                Products.Remove(p);
                Subtotal -= p.Price;
                break;
            }

        }

    }
}
