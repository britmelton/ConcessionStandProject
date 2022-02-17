using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _orders;
        public OrderRepository()
        {
            _orders = new List<Order>();
        }
        public void CreateOrder(Order order)
        {
            _orders.Add(order);
        }
    }
    
}
