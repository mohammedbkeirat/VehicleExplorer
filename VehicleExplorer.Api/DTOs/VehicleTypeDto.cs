using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class VehicleTypeDto
    {
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = string.Empty;
    }
}
