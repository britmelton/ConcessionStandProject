using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class Receipt
    {
        public Receipt(List<Product> products, Guid orderID)
        {
            Products = new List<Product>();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

        public List<Product> Products { get; set; }

    }
}
