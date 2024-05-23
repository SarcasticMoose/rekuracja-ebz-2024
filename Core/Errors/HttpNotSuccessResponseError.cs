using System.Net;
using FluentResults;

namespace Core.Errors;

public class HttpNotSuccessResponseError : Error
{
    private const string ErrorMessage = "Http response failed!";
    public HttpNotSuccessResponseError(string responseMessage, HttpStatusCode statusCode, string? reasonPhrase) : base(ErrorMessage)
    {
        Metadata.Add(nameof(responseMessage),responseMessage);
        Metadata.Add(nameof(statusCode),statusCode);
        Metadata.Add(nameof(reasonPhrase),reasonPhrase);
    }
}