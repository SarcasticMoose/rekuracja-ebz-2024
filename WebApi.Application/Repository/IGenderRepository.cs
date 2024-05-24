using WebApi.Infrastructure.Entities;

namespace WebApi.Application.Repository;

public interface IGenderRepository
{
    Task<Gender?> GetGenderByName(string name);
}