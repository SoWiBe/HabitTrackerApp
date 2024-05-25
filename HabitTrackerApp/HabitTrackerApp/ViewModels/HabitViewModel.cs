using System.Windows.Media;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Models;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitViewModel : BaseObservableElementViewModel, IHabitVm
{
    private Habit? _habit;
    private SolidColorBrush _backgroundColor;
    public HabitViewModel(SolidColorBrush color, Habit? habit = null)
    {
        _habit = habit;
        _backgroundColor = color;
    }
    
    protected override void LoadingEvent()
    {
        
    }

    public Habit? Habit
    {
        get => _habit;
        set => SetProperty(ref _habit, value);
    }

    public SolidColorBrush BackgroundColor
    {
        get => _backgroundColor;
        set => SetProperty(ref _backgroundColor, value);
    }
}