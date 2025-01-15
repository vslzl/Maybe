namespace May;

public static partial class MaybeExtensions
{
    public static async Task<Maybe<TNextValue>> Then<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, Maybe<TNextValue>> onValue)
    {
        var result = await maybe.ConfigureAwait(false);

        return result.Then(onValue);
    }
    
    public static async Task<Maybe<TNextValue>> Then<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, TNextValue> onValue)
    {
        var result = await maybe.ConfigureAwait(false);

        return result.Then(onValue);
    }
    public static async Task<Maybe<TValue>> ThenDo<TValue>(
        this Task<Maybe<TValue>> maybe,
        Action<TValue> action)
    {
        var result = await maybe.ConfigureAwait(false);

        return result.ThenDo(action);
    }
    public static async Task<Maybe<TNextValue>> ThenAsync<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe, 
        Func<TValue, Task<Maybe<TNextValue>>> onValue)
    {
        var result = await maybe.ConfigureAwait(false);

        return await result.ThenAsync(onValue).ConfigureAwait(false);
    }
    public static async Task<Maybe<TNextValue>> ThenAsync<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, Task<TNextValue>> onValue)
    {
        var result = await maybe.ConfigureAwait(false);

        return await result.ThenAsync(onValue).ConfigureAwait(false);
    }
    public static async Task<Maybe<TValue>> ThenDoAsync<TValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, Task> action)
    {
        var result = await maybe.ConfigureAwait(false);

        return await result.ThenDoAsync(action).ConfigureAwait(false);
    }
}