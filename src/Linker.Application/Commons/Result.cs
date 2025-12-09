namespace Linker.Application.Commons;

public sealed class Result<T> where T : class
{
    private readonly List<string> _errors;

    public T? Content { get; }

    public ResultType ResultType { get; }

    public bool IsSuccess => _errors.Count == 0;

    public IEnumerable<string> Errors => _errors;

    private Result(
        T content,
        ResultType resultType,
        IEnumerable<string> errors)
    {
        Content = content;
        ResultType = resultType;
        _errors = [];
        _errors.AddRange(errors);
    }

    public static Result<T> Success(T content) =>
        new(content, ResultType.SUCCESS, errors: []);

    public static Result<T> ValidationError(IEnumerable<string> errors) =>
        new(content: null!, ResultType.VALIDATION_ERROR, errors);

    public static Result<T> NotFound(string message) =>
        new(content: null!, ResultType.NOT_FOUND, errors: [message]);

    public static Result<T> InternalError() =>
        new(content: null!, ResultType.INTERNAL_ERROR, errors: [
            "Something went wrong. Try again!"
        ]);
}

public enum ResultType
{
    SUCCESS,
    VALIDATION_ERROR,
    INTERNAL_ERROR,
    NOT_FOUND
}
