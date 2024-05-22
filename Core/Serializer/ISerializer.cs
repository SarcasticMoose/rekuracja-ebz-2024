using FluentResults;

namespace Core.Serializer;

public interface ISerializer
{
    Task<Result<string>> SerializeAsync<T>(T objectToSerialize,CancellationToken ct);
    Task<Result<T>> DeserializeAsync<T>(Stream stream,CancellationToken ct);
    Task<Result<T>> DeserializeAsync<T>(string textJson,CancellationToken ct);
}