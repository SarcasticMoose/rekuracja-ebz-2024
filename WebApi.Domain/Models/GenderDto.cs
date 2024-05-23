using WebApi.Domain.Enums;

namespace WebApi.Domain.Models;

public class GenderDto
{
    public int Id { get; set; }
    public GenderNames Name { get; set; }
}