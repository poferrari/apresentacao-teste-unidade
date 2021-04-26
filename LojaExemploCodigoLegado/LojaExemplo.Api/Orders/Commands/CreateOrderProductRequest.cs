namespace LojaExemplo.Api.Orders.Commands
{
    public class CreateOrderProductRequest
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
