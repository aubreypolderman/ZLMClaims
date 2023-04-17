using System.Text.Json.Serialization;

namespace ZLMClaims.Models
{
    public class Contract
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        [JsonPropertyName("product")]
        public string Product { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("licenseplate")]
        public string LicensePlate { get; set; }

        [JsonPropertyName("damageFreeYears")]
        public int DamageFreeYears { get; set; }

        [JsonPropertyName("startingDate")]
        public DateTime StartingDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("annualPolicyPremium")]
        public double AnnualPolicyPremium { get; set; }
    }
}
