using System.Text;
using Core.Serializer;
using FluentResults;

namespace WebApi.Configuration;

public class SeedReader : ISeedReader
{
    private readonly ISerializer _serializer;

    public SeedReader(ISerializer serializer)
    {
        _serializer = serializer;
    }
    public const int BufferSize = 4096;
    
    public async Task<Result<T>> ReadFileAsync<T>(string filePath,CancellationToken ct = default!)
    {
        var sb = new StringBuilder();
        await using var sourceStream =
            new FileStream(
                filePath,
                FileMode.Open, FileAccess.Read, FileShare.None,
                bufferSize: BufferSize, useAsync: true);

        var buffer = new byte[BufferSize];
        int numRead;
        
        while ((numRead = await sourceStream.ReadAsync(buffer,0,BufferSize)) != 0)
        {
            string text = Encoding.UTF8.GetString(buffer, 0,numRead);
            sb.Append(text);
        }

        var deserializedResult = await _serializer.DeserializeAsync<T>(sb.ToString(),ct);
        if (deserializedResult.IsFailed)
        {
            return Result.Fail(deserializedResult.Errors);
        }

        var deserialized = deserializedResult.Value;
        
        return Result.Ok(deserialized);
    }
}