using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class Product
    {
        public Product(string name, double price, int sku)
        {
            Name = name;
            Price = price;  
            Sku = sku;  
        }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Sku { get; set; }

    }
}
