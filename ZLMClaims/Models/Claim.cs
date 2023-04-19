﻿using SQLite;
using System.Text.Json.Serialization;

namespace ZLMClaims.Models
{
    [Table("claims")]
    public class Claim : BaseEntity
    {

        [JsonPropertyName("dateOfOccurence")]
        public DateTime DateOfOccurence { get; set; }

        [JsonPropertyName("qWhatIsDamaged")]
        public string QWhatIsDamaged { get; set; }

        [JsonPropertyName("image1")]
        public string Image1 { get; set; }

        [JsonPropertyName("image2")]
        public string Image2 { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("contract")]
        public Contract Contract { get; set; }
    }
}
