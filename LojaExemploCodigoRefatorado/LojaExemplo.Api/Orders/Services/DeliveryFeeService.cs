using System.Net.Http;
using System.Threading.Tasks;

namespace LojaExemplo.Api.Orders.Services
{
    public class DeliveryFeeService : IDeliveryFeeService
    {
        private const string _urlApiDeliveryFee = "http://www.buscacep.correios.com.br/api-teste/calcula.php";

        public async Task<decimal> GetDeliveryFee(string zipCode)
        {
            decimal deliveryFee = 0;
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_urlApiDeliveryFee}/{zipCode}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            using (var client = new HttpClient())
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

            return deliveryFee;
        }
    }
}
