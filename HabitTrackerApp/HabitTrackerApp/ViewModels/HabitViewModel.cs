using System.Windows;
using System.Windows.Media;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Commands;
using HabitTrackerApp.Models;
using HabitTrackerApp.UI.Views.Popups;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitViewModel : BaseObservableElementViewModel, IHabitVm
{
    private Habit? _habit;
    private SolidColorBrush _backgroundColor;
    private RelayCommand _createHabitCommand;
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

    public RelayCommand CreateHabitCommand => _createHabitCommand ??= new RelayCommand(_ => CreateHabit());

    private void CreateHabit()
    {
        var popup = new CreateHabitPopup();
        popup.ShowDialog();
        
        var habit = new Habit
        {
            Title = "Test",
            CountDays = 11
        };
        
        _habit = habit;
        RaisePropertyChanged(nameof(Habit));
    }
}