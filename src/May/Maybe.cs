using System.Diagnostics.CodeAnalysis;

namespace May;

public readonly partial record struct Maybe<TValue>
{
    private readonly TValue? _value;
    private readonly Error _error;
    private readonly List<ValidationError> _validationErrors = [];
    private readonly State _state;

    public Maybe()
    {
        throw new InvalidOperationException();
    }

    private Maybe(TValue value)
    {
        _value = value;
        _state = State.Value;
    }

    private Maybe(Error error)
    {
        _error = error;
        _state = State.Error;
    }

    private Maybe(List<ValidationError> errors)
    {
        _validationErrors = errors;
        _state = State.Invalid;
    }

    public static Maybe<TValue> FromValue(TValue value)
    {
        if (value is null)
        {
            throw new InvalidOperationException("Value cannot be null.");
        }

        return new Maybe<TValue>(value);
    }

    public static Maybe<TValue> FromError(Error error)
    {
        if(error.Equals(default))
            throw new InvalidOperationException("Error cannot be null or default.");
        return new Maybe<TValue>(error);
    }

    public static Maybe<TValue> FromValidationErrors(List<ValidationError> errors)
    {
        if (errors.Count == 0)
            throw new InvalidOperationException("There must be at least one error to initialize maybe.");
        return new Maybe<TValue>(errors);
    }

    public bool Is(State state)
    {
        return (_state & state) > 0;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    public bool IsValue => (_state & State.Value) > 0;

    public TValue Value => IsValue ? _value : throw new InvalidOperationException();
    

    public bool IsError => (_state & State.Error) > 0;
    public Error Error => IsError ? _error : throw new InvalidOperationException();
    
    public bool IsInvalid => (_state & State.Invalid) > 0;
    public bool IsValid => !IsInvalid;

    public List<ValidationError> ValidationErrors =>
        IsInvalid ? _validationErrors : throw new InvalidOperationException();
    

    public static implicit operator Maybe<TValue>(TValue value) => FromValue(value);
    public static implicit operator Maybe<TValue>(Error error) => FromError(error);
    public static implicit operator Maybe<TValue>(List<ValidationError> validationErrors) => FromValidationErrors(validationErrors);
    public static implicit operator TValue(Maybe<TValue> maybe) => maybe.Value;
}