namespace WebApi.Infrastructure.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    //Relations
    public UserDetails UserDetails { get; set; }
}

