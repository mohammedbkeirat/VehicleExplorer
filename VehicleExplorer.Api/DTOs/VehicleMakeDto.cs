using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class VehicleMakeDto
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; } = string.Empty;
    }
}
