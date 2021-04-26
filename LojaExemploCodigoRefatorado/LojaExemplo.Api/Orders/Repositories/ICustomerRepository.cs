using Store.Models;
using System.Threading.Tasks;

namespace Store.Domain.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> Get(int id);
    }
}