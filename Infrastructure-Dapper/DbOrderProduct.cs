namespace Infrastructure.Dapper
{
    public class DbOrderProduct
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Sku { get; set; }
    }
}
