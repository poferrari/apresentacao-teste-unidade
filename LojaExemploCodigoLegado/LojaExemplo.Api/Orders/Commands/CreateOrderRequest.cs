using System.Collections.Generic;

namespace LojaExemplo.Api.Orders.Commands
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderProductRequest> OrderProducts { get; set; }
    }
}
