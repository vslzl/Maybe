namespace May;

public static partial class MaybeExtensions
{
    public static async Task<TNextValue> Match<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, TNextValue> onValue,
        Func<Error, TNextValue> onError,
        Func<List<ValidationError>, TNextValue> onInvalid)
    {
        var result = await maybe.ConfigureAwait(false);

        return result.Match(onValue, onError, onInvalid);
    }
    
    public static async Task<TNextValue> MatchAsync<TValue, TNextValue>(
        this Task<Maybe<TValue>> maybe,
        Func<TValue, Task<TNextValue>> onValue,
        Func<Error, Task<TNextValue>> onError,
        Func<List<ValidationError>, Task<TNextValue>> onInvalid
        )
    {
        var result = await maybe.ConfigureAwait(false);

        return await result.MatchAsync(onValue, onError, onInvalid);
    }
}