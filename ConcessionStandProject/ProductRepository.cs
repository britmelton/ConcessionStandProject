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
            yield return new Product("hotdog", 1.05, 123456);
            yield return new Product("chips", 2.25, 234567);
            yield return new Product("coca-cola", 2.95, 345678);
            yield return new Product("nachos", 3.75, 456789);
            yield return new Product("pretzel", 2.75, 567891);
            yield return new Product("cookie", 2.99, 678912);
        }
    }
}
