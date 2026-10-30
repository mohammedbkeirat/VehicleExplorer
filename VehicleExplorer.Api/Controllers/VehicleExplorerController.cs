using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleExplorer.Api.Services.Interfaces;

namespace VehicleExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleExplorerController : ControllerBase
    {
        private readonly IVehicleExplorerService _VehicleExplorerService;
        public VehicleExplorerController(IVehicleExplorerService VehicleExplorerService)
        {
            _VehicleExplorerService = VehicleExplorerService;
        }

        [HttpGet("makes")]
        public async Task<IActionResult> GetAllMakes(CancellationToken cancellationToken)
        {
            var result = await _VehicleExplorerService.GetAllMakesAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("make/{makeId}/types")]
        public async Task<IActionResult> GetVehicleTypesForMake(int makeId, CancellationToken cancellationToken)
        {
            if (makeId <= 0)
                return BadRequest("Make ID must be greater than zero.");

            var result = await _VehicleExplorerService.GetVehicleTypesForMakeAsync(makeId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("make/{makeId}/modelyear/{year}")]
        public async Task<IActionResult> GetModelsForMakeAndYear(int makeId, int year, CancellationToken cancellationToken)
        {
            if (makeId <= 0)
                return BadRequest("Make ID must be greater than zero.");

            if (year <= 0)
                return BadRequest("The model year is invalid.");

            var result = await _VehicleExplorerService.GetModelsForMakeAndYearAsync(makeId, year, cancellationToken);
            return Ok(result);
        }

        [HttpGet("make/{makeId}/modelyear/{year}/type/{type}")]
        public async Task<IActionResult> GetModelsForMakeYearAndType(int makeId, int year, string type, CancellationToken cancellationToken)
        {
            if (makeId <= 0)
                return BadRequest("Make ID must be greater than zero.");

            if (year <= 0)
                return BadRequest("The model year is invalid.");

            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Vehicle type is required.");

            var result = await _VehicleExplorerService.GetModelsForMakeYearAndTypeAsync(makeId, year, type, cancellationToken);

            return Ok(result);
        }
    }
}
