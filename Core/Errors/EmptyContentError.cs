using FluentResults;

namespace Core.Errors;

public class EmptyContentError() : Error(ErrorMessage)
{
    private const string ErrorMessage = "Content is null";
}