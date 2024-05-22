namespace WebApi.Infrastructure.Utils;

public interface IDatabaseLuncher
{
    Task Startup(CancellationToken ct);
}