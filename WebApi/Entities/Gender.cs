using Core.Models;

namespace WebApi.Entities;

public class Gender
{
    public int Id { get; set; }
    public GenderNames Name { get; set; }

    public User User { get; set; } = null!;
}