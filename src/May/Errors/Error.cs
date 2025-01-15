// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable once CheckNamespace
namespace May;

public readonly record struct Error : IError
{
    private Error(string code, string description, ErrorType type, Dictionary<string, object>? metadata)
    {
        Code = code;
        Description = description;
        Type = type;
        Metadata = metadata;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }
    public Dictionary<string, object>? Metadata { get; }


    public static Error Invalid(
        string code = "General.Invalid",
        string description = "An 'Invalid' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Invalid, metadata);
    
    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string description = "An 'Unauthorized' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Unauthorized, metadata);
    
    public static Error Forbidden(
        string code = "General.Forbidden",
        string description = "A 'Forbidden' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Forbidden, metadata);
    
    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.NotFound, metadata);
    
    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A 'Conflict' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Conflict, metadata);

    public static Error Failure(
        string code = "General.Failure",
        string description = "A failure has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Failure, metadata);
    
    public static Error Unavailable(
        string code = "General.Unavailable",
        string description = "An 'Unavailable' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.Unavailable, metadata);
    
    public static Error Critical(
        string code = "General.Critical",
        string description = "A 'Critical' error has occurred.",
        Dictionary<string, object>? metadata = null)
        => new(code, description, ErrorType.CriticalError, metadata);
    

    public static Error Custom(
        int type,
        string code,
        string description,
        Dictionary<string, object>? metadata = null)
        => new(code, description, (ErrorType)type, metadata);

    public bool Equals(Error other)
    {
        if (Type != other.Type ||
            Code != other.Code ||
            Description != other.Description)
        {
            return false;
        }

        if (Metadata is null)
        {
            return other.Metadata is null;
        }

        return other.Metadata is not null && CompareMetadata(Metadata, other.Metadata);
    }

    public override int GetHashCode() =>
        Metadata is null ? HashCode.Combine(Code, Description, Type) : ComposeHashCode();

    private int ComposeHashCode()
    {
#pragma warning disable SA1129 // HashCode needs to be instantiated this way
        var hashCode = new HashCode();
#pragma warning restore SA1129

        hashCode.Add(Code);
        hashCode.Add(Description);
        hashCode.Add(Type);

        foreach (var keyValuePair in Metadata!)
        {
            hashCode.Add(keyValuePair.Key);
            hashCode.Add(keyValuePair.Value);
        }

        return hashCode.ToHashCode();
    }

    private static bool CompareMetadata(Dictionary<string, object> metadata, Dictionary<string, object> otherMetadata)
    {
        if (ReferenceEquals(metadata, otherMetadata))
        {
            return true;
        }

        if (metadata.Count != otherMetadata.Count)
        {
            return false;
        }

        foreach (var keyValuePair in metadata)
        {
            if (!otherMetadata.TryGetValue(keyValuePair.Key, out var otherValue) ||
                !keyValuePair.Value.Equals(otherValue))
            {
                return false;
            }
        }

        return true;
    }
}