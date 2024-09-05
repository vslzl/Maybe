namespace May;

public static class MaybeFactory
{
    public static Maybe<TValue> From<TValue>(TValue value)
    {
        return value;
    }
}