using System.ComponentModel.DataAnnotations;
using Common.Entities.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Entities;

public class Habit : IEntityBase
{
    [BsonId] [BsonGuidRepresentation(GuidRepresentation.Standard)] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] [BsonElement("title")] public string Title { get; set; }
    [Required] [BsonElement("countDays")]public int CountDays { get; set; }
}