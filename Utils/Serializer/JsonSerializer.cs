using System.ComponentModel;
using System.Text;
using System.Text.Json;
using FluentResults;
using Microsoft.Extensions.Options;

namespace Core.Serializer;

public class JsonSerializer(JsonSerializerOptions jsonOptions) : ISerializer
{
    private readonly JsonSerializerOptions _jsonOptions = jsonOptions;

    public Task<Result<string>> SerializeAsync<T>(T objectToSerialize,CancellationToken ct = default!)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<T>> DeserializeAsync<T>(string textJson, CancellationToken ct = default!)
    {
        var bytes = Encoding.UTF8.GetBytes(textJson);
        Stream stream = new MemoryStream(bytes);

        return await DeserializeAsync<T>(stream,ct);
    }

    private async Task<Result<T>> DeserializeAsync<T>(Stream stream,CancellationToken ct = default!)
    {
        try
        {
            var deserialized = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions, ct);

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