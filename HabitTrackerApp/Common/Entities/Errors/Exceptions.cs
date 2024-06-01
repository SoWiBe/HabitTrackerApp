using System.Net;

namespace Common.Entities.Errors;

public class Exceptions : Exception
{
    public Exceptions(Error error)
    {
        Error = error;
        StatusCode = error.Type switch
        {
            ErrorType.Failure => 420,
            ErrorType.Unexpected => (int)HttpStatusCode.InternalServerError,
            ErrorType.Validation => (int)HttpStatusCode.UnprocessableEntity,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.BadRequest => (int)HttpStatusCode.BadRequest,
            ErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
            ErrorType.PaymentRequired => (int)HttpStatusCode.PaymentRequired,
            ErrorType.UnprocessableContent => (int)HttpStatusCode.UnprocessableEntity,
            ErrorType.Forbidden => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
    
    public int StatusCode { get; }
    public Error Error { get; }
}