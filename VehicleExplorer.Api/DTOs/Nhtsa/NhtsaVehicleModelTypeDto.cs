using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs.Nhtsa
{
    public class NhtsaVehicleModelTypeDto
    {
        [JsonPropertyName("Make_ID")]
        public int MakeId { get; set; }

        [JsonPropertyName("Make_Name")]
        public string MakeName { get; set; } = string.Empty;

        [JsonPropertyName("Model_ID")]
        public int? ModelId { get; set; }

        [JsonPropertyName("Model_Name")]
        public string ModelName { get; set; } = string.Empty;

        [JsonPropertyName("VehicleTypeId")]
        public int VehicleTypeId { get; set; }

        [JsonPropertyName("VehicleTypeName")]
        public string VehicleTypeName { get; set; } = string.Empty;
    }
}
