using LojaExemplo.Api.Orders.Repositories;
using LojaExemplo.Api.Orders.Services;
using LojaExemplo.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Repositories.Interfaces;

namespace LojaExemplo.Api.Core.Extensions
{
    public static class ContainerConfiguration
    {
        private const string SqlOrderConnectionStringName = "SqlOrder";

        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlOrderConnectionStringName = GetSqlOrderConnectionString(configuration);

            services.AddScoped<ICustomerRepository>(sp => new CustomerRepository(sqlOrderConnectionStringName));
            services.AddScoped<IDiscountRepository>(sp => new DiscountRepository(sqlOrderConnectionStringName));
            services.AddScoped<IProductRepository>(sp => new ProductRepository(sqlOrderConnectionStringName));
            services.AddScoped<IDeliveryFeeService, DeliveryFeeService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }

        private static string GetSqlOrderConnectionString(IConfiguration configuration)
            => configuration.GetConnectionString(SqlOrderConnectionStringName);
    }
}
