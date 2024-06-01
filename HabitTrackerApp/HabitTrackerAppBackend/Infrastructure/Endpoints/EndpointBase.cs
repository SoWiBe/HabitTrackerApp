using Common.Entities.Core.Error;
using Common.Entities.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerAppBackend.Infrastructure.Endpoints;


[Authorize]
[ApiController]
public abstract class EndpointBase : ControllerBase
{
    protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
    protected string DomainName => HttpContext.Request.GetDisplayUrl().Split(HttpContext.Request.Path.Value)[0];
    [NonAction]
    public virtual ActionResult GetActionResult(IErrorOr entity)
    {
        var error = entity.Errors.First();
        
        return error.Type switch
        {   
            ErrorType.BadRequest => BadRequest(error),
            ErrorType.Validation => ValidationProblem(),
            ErrorType.Conflict => Conflict(error),
            ErrorType.NotFound => NotFound(error),
            ErrorType.Unauthorized => Unauthorized(error),
            ErrorType.UnprocessableContent => UnprocessableEntity(error),
            ErrorType.Forbidden => Forbid()
        };
    }
}