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
            //arrange - prepare the code we need in order to call the method we want to test. 
            var order = new Order();
            var product = new Product();

            //act- calling the method we want to test. 
            order.Add(product);

            //assert- compare against what's expected/see if we got the expected answer
            //verify the code we wrote behaves as expected
            Assert.Equal(order.Products[0], product);
        }
    }
}
