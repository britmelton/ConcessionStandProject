using ConcessionStandProject;
using FluentAssertions;
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


        [Fact]
        public void WhenCreatingAnOrderID_ThenOrderIDIsSet()
        {
            var order = new Order();

            Assert.NotEqual(order.OrderID, Guid.Empty);
        }

        [Fact]
        public void WhenOrderIsSubmitted_ThenReceiptIsCreated()
        {
            var order = new Order();
            var product = new Product("nachos", 3.75, 456789);
            order.Add(product);
            order.Add(product);

            Receipt receipt = order.Submit();

            Assert.NotNull(receipt);
        }

        [Theory]
        [InlineData("hotdog", 1.05, 123456)]
        [InlineData("pretzel", 2.75, 567891)]
        [InlineData("nachos", 3.75, 456789)]
        [InlineData("coca-cola", 2.95, 345678)]
        public void WhenorderIsSubmitted_ThenReceiptContainsAllProducts(string name, double price, int sku)
        {
            Order order = new Order();
            Product product = new Product(name, price, sku);

            order.Add(product);

            Receipt receipt = order.Submit();

            receipt.Products.Should().BeEquivalentTo(order.Products);
            
        }

        [Fact]
        public void WhenOrderIsSubmitted_ThenReceiptContainsOrderID()
        {
            Order order = new Order();
            Product product = new Product("chips", 2.25, 234567);
            order.Add(product);

            Receipt receipt = order.Submit();

            receipt.OrderID.Should().Be(order.OrderID);
        }

        [Fact]
        public void WhenAddingAProduct_ThenSubtotalIsUpdated()
        {
            var product = new Product("hotdog", 1.05, 123456);
            var order = new Order();
            order.Add(product);

            var expectedSubtotal = product.Price;

            order.Subtotal.Should().Be(expectedSubtotal);
        }




    }
}
