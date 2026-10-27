using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class VehicleTypeDto
    {
        [JsonPropertyName("VehicleTypeId")]
        public int VehicleTypeId { get; set; }

        [JsonPropertyName("VehicleTypeName")]
        public string VehicleTypeName { get; set; } = string.Empty;
    }
}
