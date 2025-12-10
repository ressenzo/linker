namespace Linker.Link.Application.Commons;

public class Result
{
    private readonly List<string> _errors;

    public ResultType ResultType { get; }

    public bool IsSuccess => _errors.Count == 0;

    public IEnumerable<string> Errors => _errors;

    protected Result(
        ResultType resultType,
        IEnumerable<string> errors)
    {
        ResultType = resultType;
        _errors = [];
        _errors.AddRange(errors);
    }
}

public sealed class Result<T> : Result where T : class
{
    public T? Content { get; }

    private Result(
        T content,
        ResultType resultType,
        IEnumerable<string> errors) : base(
            resultType,
            errors)
    {
        Content = content;
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
