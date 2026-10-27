using System.Text.Json.Serialization;

namespace VehicleExplorer.Api.DTOs
{
    public class NhtsaResponse<T>
    {
        [JsonPropertyName("Count")]
        public int Count { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("SearchCriteria")]
        public string? SearchCriteria { get; set; }

        [JsonPropertyName("Results")]
        public List<T> Results { get; set; } = [];
    }
}
