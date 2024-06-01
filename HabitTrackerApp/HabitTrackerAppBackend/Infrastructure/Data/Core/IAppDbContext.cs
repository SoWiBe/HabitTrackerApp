using MongoDB.Driver;

namespace HabitTrackerAppBackend.Infrastructure.Data.Core;

public interface IAppDbContext
{
    IMongoDatabase GetDatabase();
}