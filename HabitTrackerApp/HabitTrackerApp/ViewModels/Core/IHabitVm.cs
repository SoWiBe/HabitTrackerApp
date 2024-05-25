using HabitTrackerApp.Models;

namespace HabitTrackerApp.ViewModels.Core;

public interface IHabitVm
{
    public Habit Habit { get; set; }
}