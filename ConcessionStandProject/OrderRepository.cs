using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly Dictionary<Guid, Order> _orders = new();

        public void CreateOrder(Order order)
        {
            _orders.Add(order.OrderId, order);
        }

        public Order Find(Guid OrderId)
        {
            return _orders[OrderId];
        }

        public void Update(Order order)
        {
            _orders[order.OrderId] = order;
            //change previous order to (=) current order (order passed in)
        }


        //public void GetAllOrders(Dictionary<Guid, Order> orders)
        //{
        //    foreach (var order in orders)
        //    {
        //        _orders.Add(order.OrderId, order);

        //    }
        //}
    }
    
}
