using FluentResults;
using Refit;

namespace Core.DataAccess;

public interface IRestApiService
{
    Task<Result<T>> ExecudeCommand<T>(Func<Task<ApiResponse<T>>> command);
}