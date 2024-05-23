using Core.Errors;
using FluentResults;
using Refit;

namespace Core.DataAccess;

public class RestApiService : IRestApiService
{
    private readonly IDataAccess _dataAccess;

    public RestApiService(
        IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<Result<T>> ExecudeCommand<T>(Func<Task<ApiResponse<T>>> command)
    {
        try
        {
            var result = await command.Invoke();

            if (result.Error != null) return Result.Fail(new HttpNotSuccessResponseError(result.Error.Message,result.StatusCode,result.Error.ReasonPhrase));

            if (result.Content is null) return Result.Fail(new Error(message: result.Error.Message));

            return Result.Ok(result.Content);
        }
        catch (ValidationApiException ex)
        {
            return Result.Fail("");
        }
        catch (ApiException ex)
        {
            return Result.Fail("");
        }
        catch (HttpRequestException ex)
        {
            return Result.Fail("");
        }
    }
}