using System.ComponentModel.DataAnnotations;
using Common.Entities.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Entities;

public class DayHabit : IEntityBase
{
    [BsonId] [BsonGuidRepresentation(GuidRepresentation.Standard)] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] [BsonElement("day")] public Day Day { get; set; }
    [Required] [BsonElement("habit")] public Habit Habit { get; set; }
    [Required] [BsonElement("isComplete")] public bool IsComplete { get; set; } = false;
}