using VehicleExplorer.Api.DTOs;

namespace VehicleExplorer.Api.Services
{
    public interface IVehicleExplorerService
    {
        Task<IList<MakeDto>> GetAllMakesAsync(CancellationToken cancellationToken);

        Task<IList<VehicleTypeDto>> GetVehicleTypesForMakeAsync(int makeId, CancellationToken cancellationToken);

        Task<IList<VehicleModelDto>> GetModelsForMakeAndYearAsync(int makeId, int year, CancellationToken cancellationToken);

        Task<IList<VehicleModelTypeDto>> GetModelsForMakeYearAndTypeAsync(int makeId, int year, string type, CancellationToken cancellationToken);
    }
}
