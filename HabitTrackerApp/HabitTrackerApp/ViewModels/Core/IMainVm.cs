using System.Collections;
using System.Collections.ObjectModel;

namespace HabitTrackerApp.ViewModels.Core;

public interface IMainVm
{
    public string Title { get; set; }
    public ReadOnlyObservableCollection<IHabitVm> Habits { get;}
    public ReadOnlyObservableCollection<int> HabitDays { get;}
}