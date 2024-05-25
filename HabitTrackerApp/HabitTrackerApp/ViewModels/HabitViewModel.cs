using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Models;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitViewModel : BaseObservableElementViewModel, IHabitVm
{
    private Habit _habit;
    public HabitViewModel(Habit habit)
    {
        _habit = habit;
    }
    
    protected override void LoadingEvent()
    {
        
    }

    public Habit Habit
    {
        get => _habit;
        set => SetProperty(ref _habit, value);
    }
}