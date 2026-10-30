using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class VehicleModelDto
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; } = string.Empty;
        public int? ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
    }
}
