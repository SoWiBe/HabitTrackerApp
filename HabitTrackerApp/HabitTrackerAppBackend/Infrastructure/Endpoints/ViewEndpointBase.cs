using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerAppBackend.Infrastructure.Endpoints;

[ApiExplorerSettings(IgnoreApi=true)]
public class ViewEndpointBase : Controller
{
    protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
    // protected bool IsAdmin => User.IsAdmin();
    protected bool IsSuccessModel => ModelState.IsValid;
    // protected bool IsUser => User.IsUser();
    protected string DomainName => HttpContext.Request.GetDisplayUrl().Split(HttpContext.Request.Path.Value)[0];
}