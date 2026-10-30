using Microsoft.AspNetCore.Http.HttpResults;
using VehicleExplorer.Api.DTOs;
using VehicleExplorer.Api.DTOs.Nhtsa;
using VehicleExplorer.Api.Services.Interfaces;

namespace VehicleExplorer.Api.Services
{
    public class VehicleExplorerService : IVehicleExplorerService
    {
        private readonly IHttpClientService _httpClientService;

        public VehicleExplorerService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IList<VehicleMakeDto>> GetAllMakesAsync(CancellationToken cancellationToken)
        {
            var NhtsaResult = await _httpClientService.GetResultsAsync<NhtsaResponse<NhtsaVehicleMakeDto>>("GetAllMakes?format=json", cancellationToken);

            return NhtsaResult.Results.Select(x => new VehicleMakeDto
            {
                MakeId = x.MakeId,
                MakeName = x.MakeName
            }).ToList();
        }

        public async Task<IList<VehicleTypeDto>>GetVehicleTypesForMakeAsync(int makeId, CancellationToken cancellationToken)
        {
            var NhtsaResult = await _httpClientService.GetResultsAsync<NhtsaResponse<VehicleTypeDto>>($"GetVehicleTypesForMakeId/{makeId}?format=json", cancellationToken);
            return NhtsaResult.Results;
        }

        public async Task<IList<VehicleModelDto>>GetModelsForMakeAndYearAsync(int makeId, int year, CancellationToken cancellationToken)
        {
            var NhtsaResult = await _httpClientService.GetResultsAsync<NhtsaResponse<NhtsaVehicleModelDto>>(
                $"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json", cancellationToken);

            return NhtsaResult.Results.Select(x => new VehicleModelDto
            {
                MakeId = x.MakeId,
                MakeName = x.MakeName,
                ModelId = x.ModelId,
                ModelName = x.ModelName
            }).ToList();
        }

        public async Task<IList<VehicleModelTypeDto>>GetModelsForMakeYearAndTypeAsync(int makeId, int year, string type, CancellationToken cancellationToken)
        {
            var encodedType = Uri.EscapeDataString(type);

            var NhtsaResult = await _httpClientService.GetResultsAsync<NhtsaResponse<NhtsaVehicleModelTypeDto>>(
                      $"GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}/vehicletype/{encodedType}?format=json", cancellationToken);

            return NhtsaResult.Results.Select(x => new VehicleModelTypeDto
            {
                MakeId = x.MakeId,
                MakeName = x.MakeName,
                ModelId = x.ModelId,
                ModelName = x.ModelName,
                VehicleTypeId =x.VehicleTypeId,
                VehicleTypeName = x.VehicleTypeName
            }).ToList();
        }
    }
}
