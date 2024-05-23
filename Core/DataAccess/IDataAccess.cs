using Core.Models;
using Refit;

namespace Core.DataAccess;

public interface IDataAccess
{
    [Post("/auth/login")]
    Task<ApiResponse<string>> Login([Body] LoginUserDto userLoginDto);
    
    [Get("/users")]
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers();
}