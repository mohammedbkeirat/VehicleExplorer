using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class VehicleModelTypeDto
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; } = string.Empty;
        public int? ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = string.Empty;
    }
}
