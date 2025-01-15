namespace May;

public readonly partial record struct Maybe<TValue>
{
    public TNextValue Match<TNextValue>(
        Func<TValue, TNextValue> onValue,
        Func<Error, TNextValue> onError,
        Func<List<ValidationError>, TNextValue> onInvalid)
    {
        if (IsInvalid)
            return onInvalid(ValidationErrors);

        if (IsError)
            return onError(Error);

        return onValue(Value);
    }

    public async Task<TNextValue> MatchAsync<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<Error, Task<TNextValue>> onError,
        Func<List<ValidationError>, Task<TNextValue>> onInvalid)
    {
        if (IsInvalid)
            return await onInvalid(ValidationErrors).ConfigureAwait(false);
        
        if (IsError)
            return await onError(Error).ConfigureAwait(false);

        return await onValue(Value).ConfigureAwait(false);
    }
}