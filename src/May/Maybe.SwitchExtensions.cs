namespace May;

public static partial class MaybeExtensions
{
    public static async Task Switch<TValue>(
        this Task<Maybe<TValue>> maybe,
        Action<TValue> onValue,
        Action<Error> onError,
        Action<List<ValidationError>> onInvalid
        )
    {
        var result = await maybe.ConfigureAwait(false);

        result.Switch(onValue, onError, onInvalid);
    }
    
    public static async Task SwitchAsync<TValue>(
        this Task<Maybe<TValue>> maybe, 
        Func<TValue, Task> onValue,
        Func<Error, Task> onError,
        Func<List<ValidationError>, Task> onInvalid
        )
    {
        var result = await maybe.ConfigureAwait(false);

        await result.SwitchAsync(onValue, onError, onInvalid);
    }
}