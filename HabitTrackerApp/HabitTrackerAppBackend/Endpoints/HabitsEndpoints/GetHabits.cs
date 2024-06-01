using System.Text.Json.Serialization;
using Common.Entities;
using HabitTrackerAppBackend.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

public class GetHabits : EndpointBaseAsync.WithoutRequest.WithActionResult<GetHabitsResponse>
{
    public GetHabits()
    {
        
    }
    
    [AllowAnonymous]
    [HttpGet("/habit")]
    public override async Task<ActionResult<GetHabitsResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(new GetHabitsResponse
        {
            Habits = new List<Habit>()
        });
    }
}

public class GetHabitsResponse
{
    [JsonPropertyName("habits")] public IEnumerable<Habit> Habits { get; set; }
}