using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Di;
using HabitTrackerApp.Extensions;
using HabitTrackerApp.Models;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class MainViewModel : BaseViewModel, IMainVm
{
    private string _title = "test";
    private readonly List<Habit> _habits = new();
    private SmartCollection<IHabitVm> _habitVms; 
    
    public MainViewModel()
    {
        
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public ReadOnlyObservableCollection<IHabitVm> Habits
    {
        get
        {
            SetHabits();
            _habitVms = new SmartCollection<IHabitVm>();
            foreach (var habit in _habits)
            {
                var habitVm =
                    AutoFac.Default.Container.Resolve<IHabitVm>(
                        new TypedParameter(typeof(Habit), habit));
                _habitVms.Add(habitVm);
            }

            return new ReadOnlyObservableCollection<IHabitVm>(_habitVms);
        }
    }

    private void SetHabits()
    {
        for (int i = 0; i < 12; i++)
        {
            var habit = new Habit { Title = $"Habit {i}", CountDays = 0};
            _habits.Add(habit);
        }
    }
}