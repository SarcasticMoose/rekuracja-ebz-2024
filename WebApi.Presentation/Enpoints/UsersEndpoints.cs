using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Application.Repository;
using WebApi.Domain.Models;
using WebApi.Infrastructure.Entities;

namespace WebApi.Enpoints;

public class UsersEndpoints() : CarterModule("/users")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", handler: async Task<Results<Ok<IEnumerable<UserDto>>, NotFound>> (HttpContext httpContent,
            IUserRepository userRepository, CancellationToken ct) =>
        {
            var users = await userRepository.GetAllUsersAsync(ct);
            if (users == null || !users.Any())
            {
                return TypedResults.NotFound();
            }

            var usersDtos = users.Select(x => new UserDto()
            {
                Id = x.Id,
                City = x.UserDetails.City,
                Country = x.UserDetails.Country,
                Created = x.UserDetails.Created,
                Username = x.Username,
                Description = x.UserDetails.Description,
                Gender = x.UserDetails.Gender.Name,
                Interests = x.UserDetails.Interests,
                Skills = x.UserDetails.Skills,
                LastActive = x.UserDetails.LastActive,
                DateOfBirth = x.UserDetails.DateOfBirth
            });

            return TypedResults.Ok(usersDtos);
        }).RequireAuthorization();
        
        app.MapGet("/{id}", handler: async Task<Results<Ok<UserDto>, NotFound>>(IUserRepository userRepository, CancellationToken ct,int id) =>
        {
            var user = await userRepository.GetUserByIdAsync(id,ct);
            if (user == null)
            {
                return TypedResults.NotFound();
            }
            
            var userDto = new UserDto()
            {
                Id = user.Id,
                City = user.UserDetails.City,
                Country = user.UserDetails.Country,
                Created = user.UserDetails.Created,
                Username = user.Username,
                Description = user.UserDetails.Description,
                Gender = user.UserDetails.Gender.Name,
                Interests = user.UserDetails.Interests,
                Skills = user.UserDetails.Skills,
                LastActive = user.UserDetails.LastActive,
                DateOfBirth = user.UserDetails.DateOfBirth
            };
            
            return TypedResults.Ok(userDto);
        }).RequireAuthorization();;

        app.MapPost("", async Task<Results<Ok, NotFound>>(IUserRepository userRepository,IGenderRepository genderRepository, CreateUserDto userDto, CancellationToken ct) =>
        {
            var user = new User()
            {
                Username = userDto.Username,
                Password = userDto.Password,
                UserDetails = new UserDetails()
                {
                    City = userDto.City,
                    Country = userDto.Country,
                    Created = userDto.Created,
                    Description = userDto.Description,
                    Gender = await genderRepository.GetGenderByName(name: userDto.Gender.ToString()),
                    Interests = userDto.Interests,
                    Skills = userDto.Skills,
                    LastActive = userDto.LastActive,
                    DateOfBirth = userDto.DateOfBirth
                }
            };
            await userRepository.AddUserAsync(user,ct);
            await userRepository.SaveChangesAsync();
            return TypedResults.Ok();
        }).RequireAuthorization();;

        app.MapPut("/{id}", async Task<Results<Ok, NotFound>>(IUserRepository userRepository,IGenderRepository genderRepository ,UpdateUserDto updateUser, int id,CancellationToken ct) =>
        {
            var user = await userRepository.GetUserByIdAsync(id, ct);

            if (user == null)
            {
                TypedResults.NotFound();
            }

            user.UserDetails.DateOfBirth = updateUser.DateOfBirth;
            user.UserDetails.Description = updateUser.Description;
            user.UserDetails.Gender = await genderRepository.GetGenderByName(name: updateUser.Gender.ToString());
            user.UserDetails.Interests = updateUser.Interests;
            user.UserDetails.Skills = updateUser.Skills;
            user.Username = updateUser.Username;
            user.UserDetails.LastActive = updateUser.LastActive;
            user.UserDetails.City = updateUser.City;
            user.UserDetails.Country = updateUser.Country;
            user.UserDetails.Created = updateUser.Created;

            await userRepository.SaveChangesAsync();
            return TypedResults.Ok();
        }).RequireAuthorization();;

        app.MapDelete("/{id}", handler: async Task<Results<Ok, NotFound>>(IUserRepository userRepository, CancellationToken ct,int id) =>
        {
            var user = await userRepository.GetUserByIdAsync(id,ct);
            if (user == null)
            {
                return TypedResults.NotFound();
            }
            userRepository.DeleteUser(user);
            await userRepository.SaveChangesAsync();
            return TypedResults.Ok();
        }).RequireAuthorization();

        app.MapPost("/change-city/{id}", async Task<Results<Ok,NotFound>> (IUserRepository userRepository, int id, CancellationToken ct,UpdateUserCityDto userDto) =>
        {
            var userToUpdate = await userRepository.GetUserByIdAsync(id, ct);
            if (userToUpdate == null)
            {
                return TypedResults.NotFound();
            }
            userToUpdate.UserDetails.City = userDto.City;
            await userRepository.SaveChangesAsync();
            return TypedResults.Ok();
        }).RequireAuthorization();;
    }
}