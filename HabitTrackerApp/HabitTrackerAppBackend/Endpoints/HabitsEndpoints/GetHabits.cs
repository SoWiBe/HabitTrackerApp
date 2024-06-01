using System.Text.Json.Serialization;
using Common.Entities;
using HabitTrackerAppBackend.Infrastructure.Data.Core;
using HabitTrackerAppBackend.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

public class GetHabits : EndpointBaseAsync.WithoutRequest.WithActionResult<GetHabitsResponse>
{
    private readonly IAppDbContext _appDbContext;

    public GetHabits(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [AllowAnonymous]
    [HttpGet("/habit")]
    public override async Task<ActionResult<GetHabitsResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var db = _appDbContext.GetDatabase();
        var collection = db.GetCollection<Habit>("Habits");
        
        var habits = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
        
        return Ok(new GetHabitsResponse
        {
            Habits = habits ?? new List<Habit>()
        });
    }
}

public class GetHabitsResponse
{
    [JsonPropertyName("habits")] public IEnumerable<Habit> Habits { get; set; }
}