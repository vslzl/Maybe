namespace May;

public readonly partial record struct Maybe<TValue>
{
    public Maybe<TNextValue> Then<TNextValue>(Func<TValue, Maybe<TNextValue>> onValue)
    {
        if (IsInvalid)
            return ValidationErrors;
        if (IsError)
            return Error;
        return onValue(Value);
    }
    
    public Maybe<TValue> ThenDo(Action<TValue> action)
    {
        if (IsInvalid)
        {
            return ValidationErrors;
        }
        
        if (IsError)
        {
            return Error;
        }

        action(Value);

        return this;
    }
    
    
    public Maybe<TNextValue> Then<TNextValue>(Func<TValue, TNextValue> onValue)
    {
        if (IsInvalid)
        {
            return ValidationErrors;
        }
        
        if (IsError)
        {
            return Error;
        }

        return onValue(Value);
    }
    
    public async Task<Maybe<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<Maybe<TNextValue>>> onValue)
    {
        if (IsInvalid)
        {
            return ValidationErrors;
        }
        
        if (IsError)
        {
            return Error;
        }

        return await onValue(Value).ConfigureAwait(false);
    }
    
    public async Task<Maybe<TValue>> ThenDoAsync(Func<TValue, Task> action)
    {
        if (IsInvalid)
        {
            return ValidationErrors;
        }
        if (IsError)
        {
            return Error;
        }

        await action(Value).ConfigureAwait(false);

        return this;
    }
    
    public async Task<Maybe<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue)
    {
        if (IsInvalid)
        {
            return ValidationErrors;
        }
        if (IsError)
        {
            return Error;
        }

        return await onValue(Value).ConfigureAwait(false);
    }
}