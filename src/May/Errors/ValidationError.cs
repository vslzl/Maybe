namespace May;

public readonly record struct ValidationError : IError
{
    private ValidationError(string code, string description)
    {
        Code = code;
        Description = description;
        Type = ErrorType.Invalid;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static ValidationError From(
        string code = "General.Validation",
        string description = "A 'Validation' error has occurred.") 
        => new(code, description);
}