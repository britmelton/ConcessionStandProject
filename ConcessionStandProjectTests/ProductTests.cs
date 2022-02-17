using ConcessionStandProject;
using System;
using Xunit;


namespace ConcessionStandProjectTests
{
    public class ProductTests
    {
        [Theory]
        [InlineData("hotdog", 1.05, 123456)]
        [InlineData("pretzel", 2.75, 567891)]
        public void WhenCreatingProduct_ThenAllPropertiesAreSet(string name, double price, int sku)
        {
            var product = new Product(name, price, sku);

            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(sku, product.Sku);
        }



    }
}
