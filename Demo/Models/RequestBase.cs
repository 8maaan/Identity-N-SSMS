using System.Text.Json.Serialization;

namespace Demo.Models
{
    public class RequestBase
    {
        // Unique ID for tracing logs
        [JsonIgnore]
        public string? CorrelationId { get; set; }

        // Timestamp when request was created
        [JsonIgnore]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}
