using System.Threading.Tasks;
using Common.Entities.Errors;
using HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

namespace HabitTrackerApp.Abstractions.Services;

public interface IHabitService
{
    Task<ErrorOr<GetHabitsResponse>> GetHabits();
}