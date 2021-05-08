using Dapper;
using Store.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;

        public DiscountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<decimal> Get(string code)
        {
            decimal discount = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                var promo = await conn.QueryFirstOrDefaultAsync<PromoCode>($"SELECT * FROM PromoCode WHERE Code='{code}'");
                if (promo != null && promo.ExpireDate > DateTime.Now)
                {
                    discount = promo.Value;
                }
            }
            return discount;
        }
    }
}
