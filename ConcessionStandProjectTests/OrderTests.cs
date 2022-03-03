using ConcessionStandProject;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConcessionStandProjectTests
{
    public class OrderTests
    {
        [Fact]
        public void WhenAddingProductToOrder_ThenProductExistsInOrder()
        {           
            var order = new Order();
            var product = new Product("cookie", 2.99, 678912, "~/css/cookie.png");

            order.Add(product);
       
            Assert.Equal(order.Products[0], product);
        }


        [Fact]
        public void WhenCreatingAnOrderID_ThenOrderIDIsSet()
        {
            var order = new Order();

            Assert.NotEqual(order.OrderId, Guid.Empty);
        }

        [Fact]
        public void WhenOrderIsSubmitted_ThenReceiptIsCreated()
        {
            var order = new Order();
            var product = new Product("nachos", 3.75, 456789, "~/css/nachos.png");
            order.Add(product);

            order.Submit();

            order.Receipt.Should().NotBeNull();
        }

        [Theory]
        [InlineData("hotdog", 1.05, 123456, "~/css/hotdog.png")]
        [InlineData("pretzel", 2.75, 567891, "~/css/pretzel.png")]
        [InlineData("nachos", 3.75, 456789, "~/css/nachos.png")]
        [InlineData("coca-cola", 2.95, 345678, "~/css/cocacola.png")]
        public void WhenorderIsSubmitted_ThenReceiptContainsAllProducts(string name, double price, int sku, string image)
        {
            Order order = new Order();
            Product product = new Product(name, price, sku, image);

            order.Add(product);
            
            order.Submit();

            order.Receipt.Products.Should().BeEquivalentTo(order.Products);
            
        }

        [Fact]
        public void WhenOrderIsSubmitted_ThenReceiptContainsOrderID()
        {
            Order order = new Order();
            Product product = new Product("chips", 2.25, 234567, "~/css/chips.png");
            
            order.Add(product);

            order.Submit();

            order.Receipt.OrderId.Should().Be(order.OrderId);
        }

        [Fact]
        public void WhenAddingAProduct_ThenSubtotalIsUpdated()
        {
            var product = new Product("hotdog", 1.05, 123456, "~/css/hotdog.png");
            var order = new Order();
            order.Add(product);

            var expectedSubtotal = product.Price;

            order.Subtotal.Should().Be(expectedSubtotal);
        }

        [Fact]
        public void WhenAddingMultipleProducts_ThenSubtotalIsUpdated()
        {
            var product = new Product("hotdog", 1.05, 123456, "~/css/hotdog.png");
            var product2 = new Product("nachos", 3.75, 456789, "~/css/nachos.png");
            var order = new Order();
            order.Add(product);
            order.Add(product2);

            var expectedSubtotal = product.Price + product2.Price;

            order.Subtotal.Should().Be(expectedSubtotal);
        }


    }
}
