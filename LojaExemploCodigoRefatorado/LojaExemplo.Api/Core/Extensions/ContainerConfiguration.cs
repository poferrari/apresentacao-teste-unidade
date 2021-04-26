using LojaExemplo.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LojaExemplo.Api.Core.Extensions
{
    public static class ContainerConfiguration
    {
        private const string SqlOrderConnectionStringName = "SqlOrder";

        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlOrderConnectionStringName = GetSqlOrderConnectionString(configuration);

            services.AddScoped<IOrderServiceLegado>(sp => new OrderServiceLegado(sqlOrderConnectionStringName));

            return services;
        }

        private static string GetSqlOrderConnectionString(IConfiguration configuration)
            => configuration.GetConnectionString(SqlOrderConnectionStringName);
    }
}
