using System.Runtime.CompilerServices;
using ConcessionStandProject;
using Xunit;


namespace ConcessionStandProjectTests
{
    public class ProductTests
    {
        private readonly Product _product;
        private readonly Product _product2;

        private const string _name = "hotdog";
        private const string _name2 = "pretzel";
        private const decimal _price = 1.05m;
        private const decimal _price2 = 2.75m;
        private const int _sku = 123456;
        private const int _sku2 = 567891;
        private const string _image = "~/css/images/hotdog.png";
        private const string _image2 = "~/css/images/pretzel.png";

        public ProductTests()
        {
            _product = new Product(_name, _price, _sku, _image);
            _product2 = new Product(_name2, _price2, _sku2, _image2);
        }

        [Fact]
        public void WhenCreatingProduct_ThenAllPropertiesAreSet()
        {
            Assert.Equal(_name, _product.Name);
            Assert.Equal(_price, _product.Price);
            Assert.Equal(_sku, _product.Sku);
            Assert.Equal(_image, _product.Image);

            Assert.Equal(_name2, _product2.Name);
            Assert.Equal(_price2, _product2.Price);
            Assert.Equal(_sku2, _product2.Sku);
            Assert.Equal(_image2, _product2.Image);
        }
    }
}