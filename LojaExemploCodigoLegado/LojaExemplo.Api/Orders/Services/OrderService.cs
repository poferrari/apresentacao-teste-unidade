using Dapper;
using LojaExemplo.Api.Models;
using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Orders.Reponses;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;

        public OrderService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<OrderMessage> SendOrder(CreateOrderRequest createOrderRequest)
        {
            // #1 - Recupera o cliente
            Customer customer = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                customer = conn.Query<Customer>
                    ("SELECT * FROM Customer WHERE ID=" + createOrderRequest.CustomerId)
                    .FirstOrDefault();
            }

            // #2 - Calcula o frete
            decimal deliveryFee = 0;
            var request = new HttpRequestMessage(HttpMethod.Get, "URL/" + createOrderRequest.ZipCode);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    deliveryFee = decimal.Parse(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    // Caso não consiga obter a taxa de entrega o valor padrão é 5
                    deliveryFee = 5;
                }
            }

            // #3 - Calcula o total dos produtos
            var orderProducts = new List<OrderProduct>();
            decimal subTotal = 0;
            for (int p = 0; p < createOrderRequest.OrderProducts.Count; p++)
            {
                var itemProduct = createOrderRequest.OrderProducts[p];

                var product = new Product();
                using (var conn = new SqlConnection(_connectionString))
                {
                    product = conn.Query<Product>
                        ("SELECT * FROM Product WHERE ID=" + itemProduct.ProductId)
                        .FirstOrDefault();
                }
                subTotal += product.Price;

                orderProducts.Add(new OrderProduct
                {
                    ProductId = itemProduct.ProductId,
                    Amount = itemProduct.Amount,
                    ProductValue = product.Price,
                    Total = itemProduct.Amount * product.Price
                });
            }

            // #4 - Aplica o cupom de desconto
            decimal discount = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                var promo = conn.Query<PromoCode>
                    ("SELECT * FROM PromoCode WHERE CODE=" + createOrderRequest.PromoCode)
                    .FirstOrDefault();
                if (promo != null && promo.ExpireDate > DateTime.Now)
                {
                    discount = promo.Value;
                }
            }

            // #5 - Gera o pedido
            var order = new Order();
            order.Code = Guid.NewGuid().ToString().ToUpper().Substring(0, 8);
            order.Date = DateTime.Now;
            order.DeliveryFee = deliveryFee;
            order.Discount = discount;
            order.OrderProducts = orderProducts;
            order.SubTotal = subTotal;

            // #6 - Calcula o total
            order.Total = subTotal - discount + deliveryFee;

            // #7 - Retorna
            return new OrderMessage
            {
                Order = order,
                Code = order.Code,
                Message = $"Pedido {order.Code} gerado com sucesso!"
            };
        }
    }
}
