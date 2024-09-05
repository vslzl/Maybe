namespace May;

public enum ErrorType
{
    Invalid = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    Failure = 500,
    Unavailable = 503,
    CriticalError = 599,
}