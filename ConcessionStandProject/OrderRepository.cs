using System;
using System.Collections.Generic;

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

        public IEnumerable<Order> GetAllOrders()
        {
 
            return _orders.Values;
        }

        public void Update(Order order)
        {
            _orders[order.OrderId] = order;
            //change previous order to (=) current order (order passed in)
        }

    

    }
    
}
