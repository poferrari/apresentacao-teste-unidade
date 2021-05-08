using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Orders.Reponses;
using LojaExemplo.Api.Orders.Repositories;
using LojaExemplo.Api.Orders.Services;
using Store.Domain.Repositories.Interfaces;
using Store.Models;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeService _deliveryFeeService;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(ICustomerRepository customerRepository,
            IDeliveryFeeService deliveryFeeService,
            IDiscountRepository discountRepository,
            IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeService = deliveryFeeService;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderMessage> SendOrder(CreateOrderRequest createOrderRequest)
        {
            // #1 - Recupera o cliente
            Customer customer = await _customerRepository.Get(createOrderRequest.CustomerId);

            // #2 - Calcula a taxa de entrega
            decimal deliveryFee = await _deliveryFeeService.GetDeliveryFee(createOrderRequest.ZipCode);

            // #3 - Obtém o cupom de desconto
            decimal discount = await _discountRepository.Get(createOrderRequest.PromoCode);

            // #4 - Obter produtos
            var orderProducts = await _productRepository.GetOrders(createOrderRequest.OrderProducts);

            // #5 - Gera o pedido
            var order = new Order(customer, deliveryFee, discount, orderProducts);

            // #6 - Retornar pedido gerado
            return new OrderMessage
            {
                Order = order,
                Code = order.Code,
                Message = $"Pedido {order.Code} gerado com sucesso!"
            };
        }
    }
}
