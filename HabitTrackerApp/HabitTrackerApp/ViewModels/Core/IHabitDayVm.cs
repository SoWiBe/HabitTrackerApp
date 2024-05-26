using System.Windows;
using System.Windows.Media;
using HabitTrackerApp.Commands;
using HabitTrackerApp.Models;

namespace HabitTrackerApp.ViewModels.Core;

public interface IHabitDayVm
{
    public string? Title { get; set; }
    public SolidColorBrush BackgroundColor { get; set; }
    public bool IsSuccess { get; set; }
    public DayHabit DayHabit { get; set; }
    public Visibility VisibilitySuccess { get; }
    RelayCommand SetHabitStatusCommand { get; }
}