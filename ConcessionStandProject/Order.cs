using System;
using System.Collections.Generic;

namespace ConcessionStandProject
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>(); //creates a new list of Products when a new Order is created.
            OrderId = Guid.NewGuid(); //creates new guid ID for each new order. 
        }

        public List<Product> Products { get; set; }
        public Guid OrderId { get; set; }
        public double Subtotal { get; set; }
        public Receipt Receipt { get; private set; }
        public double Total { get; set; }
        public bool IsCompleted { get; set; }

        public void Add(Product product)
        {
            Products.Add(product);
            CalculateSubtotal();
        }

        public void Submit()
        {
            GenerateReceipt();
            IsCompleted = true;
        }

        public void CalculateSubtotal()
        {
            double subtotal = 0;
            foreach (Product product in Products)
            {
                subtotal += product.Price;
            }

            Subtotal = subtotal;
        }

        public void GenerateReceipt()
        {
            Receipt = new Receipt(Products, OrderId);
            var total = Subtotal;
            Total = total;
        }
    }
}
