namespace May;

public readonly record struct Success;
public readonly record struct Created;
public readonly record struct Deleted;
public readonly record struct Updated;

public static class Results
{
    public static Success Success => default;

    public static Created Created => default;

    public static Deleted Deleted => default;

    public static Updated Updated => default;
}
