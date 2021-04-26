using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaExemplo.Tests.Orders.Services
{
    public class OrderServiceLegadoTests
    {
        private OrderServiceLegado _orderService;
        private const string _connectionString = "string";
        private CreateOrderRequest _createOrderRequest;
        private readonly int _customerId = 1;
        private readonly string _zipCode = "8620000";
        private readonly string _promoCode = "10OFF";

        [SetUp]
        public void Setup()
        {
            _createOrderRequest = new CreateOrderRequest
            {
                CustomerId = _customerId,
                ZipCode = _zipCode,
                PromoCode = _promoCode,
                OrderProducts = new List<CreateOrderProductRequest>
                {
                    new CreateOrderProductRequest
                    {
                        Amount = 1,
                        ProductId = 1
                    }
                }
            };

            _orderService = new OrderServiceLegado(_connectionString);
        }

        [Test]
        public async Task SendOrder_ComSucesso()
        {
            // Arrange

            // Act
            var result = await _orderService.SendOrder(_createOrderRequest);

            // Assert
            Assert.Pass();
        }
    }
}
