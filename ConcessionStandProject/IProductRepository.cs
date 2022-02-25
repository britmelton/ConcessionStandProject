﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product Find(int sku);
    }
}
