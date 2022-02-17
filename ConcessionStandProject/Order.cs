using System;
using System.Collections.Generic;

namespace ConcessionStandProject
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>(); //creates a new list of Products when a new Order is created.
            OrderID = Guid.NewGuid(); //creates new guid ID for each new order. 
        }

        public List<Product> Products { get; set; }
        public Guid OrderID { get; }


        public void Add(Product product)
        {
            Products.Add(product);
        }




    }
}
