using System.Text.Json;
using FluentResults;

namespace Core.Serializer;

public class JsonSerializer : ISerializer
{
    public Task<Result<string>> SerializeAsync<T>(T objectToSerialize,CancellationToken ct = default!)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<T>> DeserializeAsync<T>(Stream stream,CancellationToken ct = default!)
    {
        try
        {
            var deserialized = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream,JsonSerializerOptions.Default, ct);

            if (deserialized is null)
            {
                return Result.Fail(new Error("Deserialization value is null"));
            }
            
            return deserialized;
        }
        catch (JsonException ex)
        {
            return Result.Fail(new Error("Deserialization process failed with exception").CausedBy(ex));
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Something goes wrong").CausedBy(ex));
        }
    }
}