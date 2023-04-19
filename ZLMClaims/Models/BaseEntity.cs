using SQLite;
using System.Text.Json.Serialization;

namespace ZLMClaims.Models
{
    public abstract class BaseEntity
    {

        [JsonPropertyName("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

       
    }
}
