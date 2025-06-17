namespace CollectR.Application.Common;

public class Result<TValue>
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    private readonly TValue _value;

    private Result(TValue value, bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error state.", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        _value = value!;
    }

    public TValue Value =>
        IsSuccess
            ? _value
            : throw new InvalidOperationException(
                "The value of a failure result cannot be accessed."
            );

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public static Result<TValue> Failure(Error error) => new(default!, false, error);

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static implicit operator Result<TValue>(Error error) => Failure(error);
}

public static class Result
{
    public static Result<Unit> Success() => Result<Unit>.Success(Unit.Value);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);
}
