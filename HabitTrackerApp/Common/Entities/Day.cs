using Common.Entities.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Entities;

public class Day : IEntityBase
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } 
    public int Number { get; set; }
}