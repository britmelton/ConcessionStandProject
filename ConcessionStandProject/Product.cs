namespace ConcessionStandProject
{
    public class Product
    {
        public Product(string name, decimal price, int sku, string image) // There are parameters in my contructor = Theory Test 
        {
            Name = name;
            Price = price;  
            Sku = sku;
            Image = image;
           
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Sku { get; set; }
        public string Image { get; set; }


    }
}
