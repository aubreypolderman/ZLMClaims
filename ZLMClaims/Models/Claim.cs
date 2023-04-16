using System.Text.Json.Serialization;

namespace ZLMClaims.Models
{
    public class Claim
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ContractId")]
        public int? ContractId { get; set; }

        [JsonPropertyName("CauseOfDamage")]
        public string? CauseOfDamage { get; set; }

        [JsonPropertyName("ExplanationOfWhatHappened")]
        public string? ExplanationOfWhatHappened { get; set; }

        [JsonPropertyName("LongitudeAccident")]
        public int? LongitudeAccident { get; set; }

        [JsonPropertyName("LatitudeAccident")]
        public int? LatitudeAccident { get; set; }

        [JsonPropertyName("ClaimDateTime")]
        public DateTime ClaimDateTime { get; set; }

        [JsonPropertyName("Image1")]
        public string? Image1 { get; set; }

        [JsonPropertyName("Image2")]
        public string? Image2 { get; set; }

    }
}
