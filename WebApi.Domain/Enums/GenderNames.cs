using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenderNames
{
    [EnumMember(Value = "male")]
    Male,
    [EnumMember(Value = "female")]
    Female
}
