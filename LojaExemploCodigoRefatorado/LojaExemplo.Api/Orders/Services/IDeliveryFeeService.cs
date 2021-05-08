using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Services
{
    public interface IDeliveryFeeService
    {
        Task<decimal> GetDeliveryFee(string zipCode);
    }
}
