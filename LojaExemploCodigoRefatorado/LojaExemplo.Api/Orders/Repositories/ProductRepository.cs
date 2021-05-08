using Dapper;
using LojaExemplo.Api.Models;
using LojaExemplo.Api.Orders.Commands;
using Store.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<OrderProduct>> GetOrders(IList<CreateOrderProductRequest> requestOrderProducts)
        {
            var orderProducts = new List<OrderProduct>();
            for (var p = 0; p < requestOrderProducts.Count; p++)
            {
                var itemProduct = requestOrderProducts[p];

                var product = new Product();
                using (var conn = new SqlConnection(_connectionString))
                {
                    product = await conn.QueryFirstAsync<Product>($"SELECT * FROM Product WHERE Id=@id", new { id = itemProduct.ProductId });
                }

                orderProducts.Add(new OrderProduct
                {
                    ProductId = itemProduct.ProductId,
                    Amount = itemProduct.Amount,
                    ProductValue = product.Price,
                    Total = itemProduct.Amount * product.Price
                });
            }
            return orderProducts;
        }
    }
}
