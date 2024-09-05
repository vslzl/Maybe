namespace May;

public interface IError
{
    string Code { get; }
    string Description { get; }
    ErrorType Type { get; }
}