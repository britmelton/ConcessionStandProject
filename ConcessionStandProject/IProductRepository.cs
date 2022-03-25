using System.Collections.Generic;

namespace ConcessionStandProject
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product Find(int sku);
    }
}
