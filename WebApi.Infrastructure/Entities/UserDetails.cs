namespace WebApi.Infrastructure.Entities;

public class UserDetails
{
    public int Id { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    
    //Relations
    public int GenderId { get; set; }
    public Gender? Gender { get; set; } = new();
    public int UserId { get; set; } 
    public User? User { get; set; }
}