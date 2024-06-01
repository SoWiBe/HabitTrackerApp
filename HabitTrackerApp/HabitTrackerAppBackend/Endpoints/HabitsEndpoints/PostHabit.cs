using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.Entities;
using HabitTrackerAppBackend.Infrastructure.Data.Core;
using HabitTrackerAppBackend.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

public class PostHabit : EndpointBaseAsync.WithRequest<PostHabitRequest>.WithActionResult<PostHabitResponse>
{
    private readonly IAppDbContext _appDbContext;

    public PostHabit(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [AllowAnonymous]
    [HttpPost("/habit")]
    public override async Task<ActionResult<PostHabitResponse>> HandleAsync(PostHabitRequest request, CancellationToken cancellationToken = default)
    {
        var db = _appDbContext.GetDatabase();
        var collection = db.GetCollection<Habit>("Habits");
        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            CountDays = 0
        };
        
        await collection.InsertOneAsync(habit, cancellationToken: cancellationToken);

        return Ok(new PostHabitResponse
        {
            Habit = habit
        });
    }
}

public class PostHabitRequest
{
    [Required] [JsonPropertyName("title")] public string Title { get; set; }
}

public class PostHabitResponse
{
    [JsonPropertyName("Habit")] public Habit Habit { get; set; }
}