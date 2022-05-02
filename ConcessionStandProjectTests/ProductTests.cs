using ConcessionStandProject;
using System;
using Xunit;


namespace ConcessionStandProjectTests
{
    public class ProductTests
    {
        [Theory]
        [InlineData("hotdog", 1.05, 123456, "~/css/images/hotdog.png")]
        [InlineData("pretzel", 2.75, 567891, "~/css/images/pretzel.png")]
        public void WhenCreatingProduct_ThenAllPropertiesAreSet(string name, double price, int sku, string image)
        {
            var product = new Product(name, price, sku, image);

            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(sku, product.Sku);
            Assert.Equal(image, product.Image);
        }



    }
}
