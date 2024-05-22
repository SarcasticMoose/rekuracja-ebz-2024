using System.Text;
using Core.Serializer;

namespace WebApi.Configuration;

public class SeedReader : ISeedReader
{
    private readonly ISerializer _serializer;

    public SeedReader(ISerializer serializer)
    {
        _serializer = serializer;
    }
    public const int BufferSize = 4096;
    
    public async Task<string> ReadFileAsync(string filePath)
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
            string text = Encoding.Unicode.GetString(buffer, 0,numRead);
            sb.Append(text);
        }
        return sb.ToString();
    }
}