using ConcessionStandProject;
using System;
using Xunit;

namespace ConcessionStandProjectTests
{
    public class OrderTests
    {
        [Fact]
        public void WhenAddingProductToOrder_ThenProductExistsInOrder()
        {           
            var order = new Order();
            var product = new Product("cookie", 2.99, 678912);

            order.Add(product);
       
            Assert.Equal(order.Products[0], product);
        }
    }
}
