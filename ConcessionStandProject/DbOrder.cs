using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class DbOrder
    {
        public string OrderId { get; set; }
        public double Total { get; set; }
        public bool IsCompleted { get; set; }
    }
}
