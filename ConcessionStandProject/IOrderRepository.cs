using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionStandProject
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public Order Find(Guid OrderId);

        public void Update(Order order);

        IEnumerable<Order> GetAllOrders();
    }
}
