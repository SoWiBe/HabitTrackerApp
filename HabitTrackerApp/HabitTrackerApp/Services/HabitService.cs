using Common.Abstraction.Repositories;
using HabitTrackerApp.Abstractions.Services;
using HabitTrackerApp.Repositories.Core;

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

}