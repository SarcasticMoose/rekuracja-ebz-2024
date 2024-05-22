using FluentResults;

namespace WebApi.Configuration;

public interface ISeedReader
{
    Task<Result<T>> ReadFileAsync<T>(string filePath, CancellationToken ct = default!);
}