namespace May;

public readonly partial record struct Maybe<TValue>
{
    public void Switch(Action<TValue> onValue, Action<Error> onError, Action<List<ValidationError>> onInvalid)
    {
        if (IsInvalid)
        {
            onInvalid(ValidationErrors);
            return;
        }
        if (IsError)
        {
            onError(Error);
            return;
        }

        onValue(Value);
    }
    
    public async Task SwitchAsync(
        Func<TValue, Task> onValue,
        Func<Error, Task> onError,
        Func<List<ValidationError>, Task> onInvalid
        )
    {
        if (IsInvalid)
        {
            await onInvalid(ValidationErrors).ConfigureAwait(false);
            return;
        }
        
        if (IsError)
        {
            await onError(Error).ConfigureAwait(false);
            return;
        }

        await onValue(Value).ConfigureAwait(false);
    }
}