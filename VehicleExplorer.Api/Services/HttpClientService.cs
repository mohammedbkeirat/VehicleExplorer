using VehicleExplorer.Api.DTOs;
using VehicleExplorer.Api.Services.Interfaces;

namespace VehicleExplorer.Api.Services
{
    public class HttpClientService: IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientName;

        public HttpClientService(IHttpClientFactory httpClientFactory, string clientName)
        {
            _httpClientFactory = httpClientFactory;
            _clientName = clientName;
        }

        public async Task<T> GetResultsAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(_clientName);

            var response = await httpClient.GetFromJsonAsync<T>(
                requestUri,
                cancellationToken);

            if (response is null)
            {
                throw new HttpRequestException("Empty API response.");
            }

            return response;
        }
    }
}
