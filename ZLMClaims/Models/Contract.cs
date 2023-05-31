using System.Text.Json.Serialization;

namespace ZLMClaims.Models;

public class Contract
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("personId")]
    public int PersonId { get; set; }

    [JsonPropertyName("product")]
    public string Product { get; set; }

    [JsonPropertyName("make")]
    public string Make { get; set; }
        
    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("licensePlate")]
    public string LicensePlate { get; set; }

    [JsonPropertyName("damageFreeYears")]
    public int DamageFreeYears { get; set; }

    [JsonPropertyName("startingDate")]
    public DateTime StartingDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("annualPolicyPremium")]
    public double AnnualPolicyPremium { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; }

    // Add method to determine icon?
}
