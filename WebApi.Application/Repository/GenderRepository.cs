using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Enums;
using WebApi.Infrastructure.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Application.Repository;

public class GenderRepository(DataContext dataContext) : IGenderRepository
{
    public async Task<Gender?> GetGenderByName(string name)
    {
        return await dataContext.Genders.FirstOrDefaultAsync(x => x.Name == Enum.Parse<GenderNames>(name));
    }
}