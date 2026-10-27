using VehicleExplorer.Api.DTOs;

namespace VehicleExplorer.Api.Services
{
    public class VehicleExplorerService : IVehicleExplorerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleExplorerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<MakeDto>> GetAllMakesAsync(CancellationToken cancellationToken)
        {
           throw new NotImplementedException();
        }

        public async Task<IList<VehicleTypeDto>>GetVehicleTypesForMakeAsync(int makeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VehicleModelDto>>GetModelsForMakeAndYearAsync(int makeId, int year, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VehicleModelTypeDto>>GetModelsForMakeYearAndTypeAsync(int makeId, int year, string type, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task<IReadOnlyList<T>> GetResultsAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("Nhtsa");

            var response = await httpClient.GetFromJsonAsync<NhtsaResponse<T>>(
                requestUri,
                cancellationToken);

            if (response is null)
            {
                throw new HttpRequestException(
                    "The NHTSA API returned an empty response.");
            }

            return response.Results;
        }
    }
}
