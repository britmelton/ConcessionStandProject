using System;
using System.Collections.Generic;

namespace ConcessionStandProject
{
    public class Receipt
    {
        public Receipt(List<Product> products, Guid orderId)   //constructor 
        {
            Products = new List<Product>();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
            OrderId = orderId;
        }

        public List<Product> Products { get; set; } //properties 
        public Guid OrderId { get; }
    }
}
