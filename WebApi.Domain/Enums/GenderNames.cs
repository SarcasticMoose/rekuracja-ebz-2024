using System.Runtime.Serialization;

namespace WebApi.Domain.Enums;

public enum GenderNames
{
    [EnumMember(Value = "male")]
    Male,
    [EnumMember(Value = "female")]
    Female
}
