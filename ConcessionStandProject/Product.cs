namespace ConcessionStandProject
{
    public class Product
    {
        public Product(string name, double price, int sku, string image)
        {
            Name = name;
            Price = price;  
            Sku = sku;
            Image = image;
           
        }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Sku { get; set; }
        public string Image { get; set; }


    }
}
