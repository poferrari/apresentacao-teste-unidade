using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Repositories
{
    public interface IDiscountRepository
    {
        Task<decimal> Get(string code);
    }
}
