using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media;
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

    private SolidColorBrush _firstColor;
    private SolidColorBrush

 _secondColor;

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
            SetColors();
            SetHabits();
            _habitVms = new SmartCollection<IHabitVm>();
            for (var i = 0; i < _habits.Count; i++)
            {
                var color = _firstColor;
                if (i % 2 != 0)
                    color = _secondColor;
                
                var habitVm = AutoFac.Default.Container.Resolve<IHabitVm>(
                    new TypedParameter(typeof(SolidColorBrush), color), new TypedParameter(typeof(int), 30));
                _habitVms.Add(habitVm);
            }

            return new ReadOnlyObservableCollection<IHabitVm>(_habitVms);
        }
    }

    public ReadOnlyObservableCollection<int> HabitDays { get; }

    private void SetHabits()
    {
        for (int i = 0; i < 12; i++)
        {
            var habit = new Habit { Title = $"Habit {i}", CountDays = 0};
            _habits.Add(habit);
        }
    }

    private void SetColors()
    {
        _firstColor = new SolidColorBrush((Color)Application.Current.Resources["MainColor"]);
        _secondColor = new SolidColorBrush((Color)Application.Current.Resources["AlternativeColor"]);
    }
}