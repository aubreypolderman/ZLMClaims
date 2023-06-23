using System.Text.Json.Serialization;

namespace ZLMClaims.Models;
public class ClaimForm
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public int? ContractId { get; set; }
    [JsonPropertyName("dateOfOccurence")]
    public DateTime? DateOfOccurence { get; set; }
    [JsonPropertyName("QCauseOfDamage")]
    public string? QCauseOfDamage { get; set; }
    public string? QWhatHappened { get; set; }
    public string? QWhereDamaged { get; set; }
    public string? QWhatIsDamaged { get; set; }
    [JsonPropertyName("image1")]
    public string? Image1 { get; set; }
    [JsonPropertyName("image2")]
    public string? Image2 { get; set; }
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    [JsonPropertyName("suite")]
    public string? Suite { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("zipcode")]
    public string? ZipCode { get; set; }
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
    [JsonPropertyName("contract")]
    public Contract Contract { get; set; } = new Contract(); // Initialize Contract property
    public User User { get; set; } // Gebruik de bestaande User-klasse
}
