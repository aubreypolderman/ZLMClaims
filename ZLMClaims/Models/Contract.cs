using System.Text.Json.Serialization;

namespace ZLMClaims.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public int DamageFreeYears { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AnnualPolicyPremium { get; set; }
    }
}
