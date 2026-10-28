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
            return await GetResultsAsync<MakeDto>("GetAllMakes?format=json", cancellationToken);
        }

        public async Task<IList<VehicleTypeDto>>GetVehicleTypesForMakeAsync(int makeId, CancellationToken cancellationToken)
        {
            return await GetResultsAsync<VehicleTypeDto>($"GetVehicleTypesForMakeId/{makeId}?format=json", cancellationToken);
        }

        public async Task<IList<VehicleModelDto>>GetModelsForMakeAndYearAsync(int makeId, int year, CancellationToken cancellationToken)
        {
            return await GetResultsAsync<VehicleModelDto>(
                $"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json", cancellationToken);
        }

        public async Task<IList<VehicleModelTypeDto>>GetModelsForMakeYearAndTypeAsync(int makeId, int year, string type, CancellationToken cancellationToken)
        {
            var encodedType = Uri.EscapeDataString(type);

            return await GetResultsAsync<VehicleModelTypeDto>(
                      $"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}/vehicletype/{encodedType}?format=json", cancellationToken);
        }

        private async Task<IList<T>> GetResultsAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("Nhtsa");

            var response = await httpClient.GetFromJsonAsync<NhtsaResponse<T>>(
                requestUri,
                cancellationToken);

            if (response is null)
            {
                throw new HttpRequestException("The NHTSA API returned an empty response.");
            }

            return response.Results;
        }
    }
}
