using HabitTrackerAppBackend.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

public class PostHabit : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    [AllowAnonymous]
    [HttpPost("/habit")]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}