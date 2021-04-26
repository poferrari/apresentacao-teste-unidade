using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Orders.Reponses;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Services
{
    public interface IOrderService
    {
        Task<OrderMessage> SendOrder(CreateOrderRequest createOrderRequest);
    }
}
