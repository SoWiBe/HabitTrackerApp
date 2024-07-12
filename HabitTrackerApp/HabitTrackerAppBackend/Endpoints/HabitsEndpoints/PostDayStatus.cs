using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.Entities;
using Common.Entities.Errors;
using HabitTrackerAppBackend.Infrastructure.Data.Core;
using HabitTrackerAppBackend.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

public class PostDayStatus : EndpointBaseAsync.WithRequest<PostDayStatusRequest>.WithActionResult
{
    private readonly IAppDbContext _appDbContext;

    public PostDayStatus(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [AllowAnonymous]
    [HttpPost(PostDayStatusRequest.Route)]
    public override async Task<ActionResult> HandleAsync(PostDayStatusRequest request, CancellationToken cancellationToken = default)
    {
        var db = _appDbContext.GetDatabase();
        var collection = db.GetCollection<Habit>("Habits");

        var habit = await collection.Find(x => x.Title.Equals(request.Title)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (habit is null)
            return GetActionResult(ErrorOr.From(Error.NotFound("habits.notfound", "habit was not found")));

        var days = new List<DayHabit>();
        if (habit.Days is not null)
            days = habit.Days.ToList();
        
        days.Add(new DayHabit
        {
            Habit = habit,
            IsComplete = request.IsComplete,
            Day = new Day { Number = request.Number }
        });
        
        var filter = Builders<Habit>.Filter.Eq("title", request.Title);
        var update = Builders<Habit>.Update.Set("days", days);

        await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        
        var habits = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);

        return Ok(new GetHabitsResponse
        {
            Habits = habits ?? new List<Habit>()
        });
    }
}

public class PostDayStatusRequest
{
    public const string Route = "/day/status";
    
    [Required] [JsonPropertyName("title")] public string Title { get; set; }
    [Required] [JsonPropertyName("date")] public int Number { get; set; }
    [Required] [JsonPropertyName("isComplete")] public bool IsComplete { get; set; }
}