using System.Windows.Media;
using HabitTrackerApp.Commands;

namespace HabitTrackerApp.ViewModels.Core;

public interface IHabitDayVm
{
    public string? Title { get; set; }
    public SolidColorBrush BackgroundColor { get; set; }
    RelayCommand SetHabitStatusCommand { get; }
}