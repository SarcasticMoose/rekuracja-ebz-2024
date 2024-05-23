using WebApi.Domain.Enums;

namespace WebApi.Infrastructure.Entities;

public class Gender
{
    public int Id { get; set; }
    public GenderNames Name { get; set; }

    //Relations
    public ICollection<UserDetails> Users { get; set; } = new List<UserDetails>();
}