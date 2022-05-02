using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class DbOrderProduct
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Sku { get; set; }
    }
}
