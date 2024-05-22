using WebApi.Domain.Enums;

namespace WebApi.Infrastructure.Entities;

public class Gender
{
    public int Id { get; set; }
    public GenderNames Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}