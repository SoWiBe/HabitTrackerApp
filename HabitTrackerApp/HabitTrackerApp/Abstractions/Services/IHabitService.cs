using System.Threading.Tasks;
using Common.Entities.Core.Error;
using Common.Entities.Errors;
using HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

namespace HabitTrackerApp.Abstractions.Services;

public interface IHabitService
{
    Task<ErrorOr<GetHabitsResponse>> GetHabits();
    Task<IErrorOr> PostDayStatus(PostDayStatusRequest request);
}