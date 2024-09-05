namespace May;

public static partial class MaybeExtensions
{
    public static Maybe<TValue> ToMaybe<TValue>(this TValue value)
    {
        return value;
    }

    public static Maybe<TValue> ToMaybe<TValue>(this Error error)
    {
        return error;
    }

    public static Maybe<TValue> ToMaybe<TValue>(this List<ValidationError> errors)
    {
        return errors;
    }

    public static Maybe<TValue> ToMaybe<TValue>(this ValidationError[] errors)
    {
        if (errors is { Length: > 0 })
            return errors.ToList();
        throw new InvalidOperationException();
    }
}