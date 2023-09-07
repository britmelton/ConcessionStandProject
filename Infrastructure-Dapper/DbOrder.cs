namespace Infrastructure.Dapper
{
    public class DbOrder
    {
        public string OrderId { get; set; }
        public decimal Total { get; set; }
        public bool IsCompleted { get; set; }
    }
}
