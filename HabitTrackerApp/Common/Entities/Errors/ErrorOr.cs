using Common.Entities.Core.Error;

namespace Common.Entities.Errors;

public record struct ErrorOr<TValue> : IErrorOr
{
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors = null;

    public static readonly Error NoFirstError = Error.Unexpected(
        code: "ErrorOr.NoFirstError",
        description: "First error cannot be retrieved from a successful ErrorOr.");

    public static readonly Error NoErrors = Error.Unexpected(
        code: "ErrorOr.NoErrors",
        description: "Error list cannot be retrieved from a successful ErrorOr.");

    public bool IsError { get; }

    public List<Error> Errors => IsError ? _errors! : new List<Error> { NoErrors };

    public List<Error> ErrorsOrEmptyList => IsError ? _errors! : new();

    public static ErrorOr<TValue> From(List<Error> errors)
    {
        return errors;
    }

    public TValue Value => _value!;

    public Error FirstError => !IsError ? NoFirstError : _errors![0];

    private ErrorOr(Error error)
    {
        _errors = new List<Error> { error };
        IsError = true;
    }

    private ErrorOr(List<Error> errors)
    {
        _errors = errors;
        IsError = true;
    }

    private ErrorOr(TValue value)
    {
        _value = value;
        IsError = false;
    }

    public static implicit operator ErrorOr<TValue>(TValue value)
    {
        return new ErrorOr<TValue>(value);
    }

    public static implicit operator ErrorOr<TValue>(Error error)
    {
        return new ErrorOr<TValue>(error);
    }

    public static implicit operator ErrorOr<TValue>(List<Error> errors)
    {
        return new ErrorOr<TValue>(errors);
    }

    public static implicit operator ErrorOr<TValue>(Error[] errors)
    {
        return new ErrorOr<TValue>(errors.ToList());
    }

    public void Switch(Action<TValue> onValue, Action<List<Error>> onError)
    {
        if (IsError)
        {
            onError(Errors);
            return;
        }

        onValue(Value);
    }

    public async Task SwitchAsync(Func<TValue, Task> onValue, Func<List<Error>, Task> onError)
    {
        if (IsError)
        {
            await onError(Errors).ConfigureAwait(false);
            return;
        }

        await onValue(Value).ConfigureAwait(false);
    }

    public void SwitchFirst(Action<TValue> onValue, Action<Error> onFirstError)
    {
        if (IsError)
        {
            onFirstError(FirstError);
            return;
        }

        onValue(Value);
    }

    public async Task SwitchFirstAsync(Func<TValue, Task> onValue, Func<Error, Task> onFirstError)
    {
        if (IsError)
        {
            await onFirstError(FirstError).ConfigureAwait(false);
            return;
        }

        await onValue(Value).ConfigureAwait(false);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError)
    {
        return IsError ? onError(Errors) : onValue(Value);
    }

    public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onValue,
        Func<List<Error>, Task<TResult>> onError)
    {
        return IsError 
            ? await onError(Errors).ConfigureAwait(false)
            : await onValue(Value).ConfigureAwait(false);
    }

    public TResult MatchFirst<TResult>(Func<TValue, TResult> onValue, Func<Error, TResult> onFirstError)
    {
        return IsError ? onFirstError(FirstError) : onValue(Value);
    }

    public async Task<TResult> MatchFirstAsync<TResult>(Func<TValue, Task<TResult>> onValue,
        Func<Error, Task<TResult>> onFirstError)
    {
        return IsError
            ? await onFirstError(FirstError).ConfigureAwait(false) 
            : await onValue(Value).ConfigureAwait(false);
    }
}

public static class ErrorOr
{
    public static ErrorOr<TValue> From<TValue>(TValue value)
    {
        return value;
    }
    
    public static IErrorOr From(Error error)
    {
        ErrorOr<bool> result = error;
        return result;
    }

    public static IErrorOr NoError()
    {
        ErrorOr<bool> result = true;
        return result;
    }
}