using System.Runtime.Serialization;

namespace Core.Models;

public enum GenderNames
{
    [EnumMember(Value = "male")]
    Male,
    [EnumMember(Value = "female")]
    Female
}
