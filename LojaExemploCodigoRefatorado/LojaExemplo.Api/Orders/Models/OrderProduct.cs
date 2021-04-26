namespace LojaExemplo.Api.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductValue { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
    }
}
