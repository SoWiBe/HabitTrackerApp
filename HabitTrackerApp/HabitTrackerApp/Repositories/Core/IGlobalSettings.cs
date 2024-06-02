namespace HabitTrackerApp.Repositories.Core;

public interface IGlobalSettings
{
    string Language { get; }
    string ApiUrl { get; }
}
