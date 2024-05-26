using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HabitTrackerApp.Models;

namespace HabitTrackerApp.ViewModels.Core;

public interface IMainVm
{
    public string Title { get; set; }
    public ReadOnlyObservableCollection<IHabitVm> Habits { get;}
    public List<Day> MonthDays { get;}
}