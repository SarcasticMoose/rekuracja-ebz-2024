using Core.Models;

namespace WebApi.Entities;

public class GenderEntity
{
    public int Id { get; set; }
    public GenderNames Name { get; set; }

    public UserEntity User { get; set; } = null!;
}