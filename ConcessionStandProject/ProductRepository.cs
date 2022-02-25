using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return _products.Values;
        }

        private static Dictionary<int, Product> _products = new Dictionary<int, Product>(); //field
        static ProductRepository()
        {
            _products.Add(123456, new Product("hotdog", 1.05, 123456, "~/css/images/hotdog.jpg"));
            _products.Add(234567, new Product("chips", 2.25, 234567, "~/css/images/chips.jpg"));
            _products.Add(345678, new Product("cocacola", 2.95, 345678, "~/css/images/cocacola.jpg"));
            _products.Add(456789, new Product("nachos", 3.75, 456789, "~/css/images/nachos.jpg"));
            _products.Add(567891, new Product("pretzle", 2.75, 567891, "~/css/images/pretzel.jpg"));
            _products.Add(678912, new Product("cookie", 2.99, 678912, "~/css/images/cookie.jpg"));
        }
        public Product Find(int sku)
        {
            return _products[sku];
        }
    }
}
