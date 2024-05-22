using System.Text.Json.Serialization;
using WebApi.Domain.Enums;

namespace WebApi.Domain.Models;

public class User
{
    public int Id { get; set; }
    [JsonPropertyName("UserName")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("Gender")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GenderNames Gender { get; set; } = new();
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}