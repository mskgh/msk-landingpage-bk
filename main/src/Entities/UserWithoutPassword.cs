using System.Text.Json.Serialization;

namespace main.src.Entities
{
    public class UserWithoutPassword
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("TernantId")]
        public Guid TernantId { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; } = null!;
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = null!;
        [JsonPropertyName("OtherNames")]
        public string OtherNames { get; set; }
        [JsonPropertyName("Email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("MobileNumber")]
        public string MobileNumber { get; set; }

    }
}
