using System.Threading.Tasks;
using Common.Abstraction.Repositories;
using Common.Entities.Core.Error;
using Common.Entities.Errors;
using HabitTrackerApp.Abstractions.Services;
using HabitTrackerApp.Repositories.Core;
using HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

namespace HabitTrackerApp.Services;

public class HabitService : IHabitService
{
    private readonly IGlobalSettings _gs;
    private readonly IApiRepository _apiRepository;

    public HabitService(IGlobalSettings gs, IApiRepository apiRepository)
    {
        _gs = gs;
        _apiRepository = apiRepository;
    }
    
    public async Task<ErrorOr<GetHabitsResponse>> GetHabits()
    {
        var url = _gs.ApiUrl + "/habit";
        var result = await _apiRepository.GetResponseAsync<GetHabitsResponse>(url);
        if (result.IsError)
            return result.FirstError;

        return result.Value;
    }

    public async Task<IErrorOr> PostDayStatus(PostDayStatusRequest request)
    {
        var url = _gs.ApiUrl + "/day/status";
        var result = await _apiRepository.PostDataWithResponseAsync(url, request);
        if (result.IsError)
            return ErrorOr.From(result.FirstError);

        return ErrorOr.From(result.FirstError);
    }
}