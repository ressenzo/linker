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

    public static Result Success() =>
        new(ResultType.SUCCESS, errors: []);

    public static Result NotFound(string message) =>
        new(ResultType.NOT_FOUND, errors: [message]);

    public static Result ValidationError(IEnumerable<string> errors) =>
        new(ResultType.VALIDATION_ERROR, errors);
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

    public static new Result<T> ValidationError(IEnumerable<string> errors) =>
        new(content: null!, ResultType.VALIDATION_ERROR, errors);

    public static new Result<T> NotFound(string message) =>
        new(content: null!, ResultType.NOT_FOUND, errors: [message]);
}

public enum ResultType
{
    SUCCESS,
    VALIDATION_ERROR,
    INTERNAL_ERROR,
    NOT_FOUND
}
