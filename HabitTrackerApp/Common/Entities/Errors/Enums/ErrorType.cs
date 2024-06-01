namespace Common.Entities.Errors;

public enum ErrorType
{
    Failure = 0,
    Unexpected = 1,
    Validation = 2,
    Conflict = 3,
    NotFound = 4,
    BadRequest = 5,
    Unauthorized = 6,
    PaymentRequired = 7,
    UnprocessableContent = 8,
    Forbidden = 9
}