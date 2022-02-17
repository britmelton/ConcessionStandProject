using System;
using System.Collections.Generic;

namespace ConcessionStandProject
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>(); //creates a new list of Products when a new Order is created.
        }

        public List<Product> Products { get; set; }


        public void Add(Product product)
        {
            Products.Add(product);
        }




    }
}
