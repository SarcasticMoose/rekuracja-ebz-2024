using FluentResults;

namespace WebApi.Configuration;

public interface IFileReader
{
    Task<Result<string>> ReadFileAsyncToString(string filePath, CancellationToken ct = default!);
}