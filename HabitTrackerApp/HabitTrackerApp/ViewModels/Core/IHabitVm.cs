using System.Collections.ObjectModel;
using System.Windows.Media;
using Common.Entities;
using HabitTrackerApp.Commands;

namespace HabitTrackerApp.ViewModels.Core;

public interface IHabitVm
{
    public Habit? Habit { get; set; }
    public SolidColorBrush BackgroundColor { get; set; }
    RelayCommand CreateHabitCommand { get; }
    public ReadOnlyObservableCollection<IHabitDayVm> HabitDays { get;  }
}