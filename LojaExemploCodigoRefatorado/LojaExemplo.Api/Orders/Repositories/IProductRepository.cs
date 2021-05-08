using LojaExemplo.Api.Models;
using LojaExemplo.Api.Orders.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Repositories
{
    public interface IProductRepository
    {
        Task<List<OrderProduct>> GetOrders(IList<CreateOrderProductRequest> requestOrderProducts);
    }
}
