using Store.Models;

namespace LojaExemplo.Api.Orders.Reponses
{
    public class OrderMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Order Order { get; set; }
    }
}
