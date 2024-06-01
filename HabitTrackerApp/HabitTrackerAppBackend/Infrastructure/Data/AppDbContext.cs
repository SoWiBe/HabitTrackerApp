using HabitTrackerAppBackend.Infrastructure.Data.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HabitTrackerAppBackend.Infrastructure.Data;

public class AppDbContext : IAppDbContext
{
    private readonly MongoClient _client;

    public AppDbContext(string connection)
    {
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        _client = new MongoClient(connection);
    }
    
    public IMongoDatabase GetDatabase() => _client.GetDatabase("HabitsDb");
}