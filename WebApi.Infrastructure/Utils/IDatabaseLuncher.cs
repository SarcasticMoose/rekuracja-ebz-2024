using FluentResults;

namespace WebApi.Infrastructure.Utils;

public interface IDatabaseLuncher
{
    Task<Result> Startup(CancellationToken ct);
}