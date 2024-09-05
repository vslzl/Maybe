namespace May;

public enum ResultStatus
{
    Ok = 200,
    Created = 201,
    NoContent = 204,
    Invalid = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    Failure = 500,
    Unavailable = 503,
    CriticalError = 599,
    MultipleErrors = 999,
    NonMatch = 1001,
}