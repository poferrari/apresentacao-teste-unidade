using Dapper;
using Store.Domain.Repositories.Interfaces;
using Store.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Customer> Get(int id)
        {
            Customer customer = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                customer = await conn.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM dbo.Customer WHERE Id=@id",
                    new { id });
            }
            return customer;
        }
    }
}
