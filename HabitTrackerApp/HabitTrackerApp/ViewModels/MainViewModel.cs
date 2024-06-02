using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media;
using Autofac;
using Common.Entities;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Abstractions.Services;
using HabitTrackerApp.Di;
using HabitTrackerApp.Extensions;
using HabitTrackerApp.Services;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class MainViewModel : BaseViewModel, IMainVm
{
    private readonly IHabitService _habitService;
    private string _title = "test";
    private readonly List<Habit> _habits = new();
    private SmartCollection<IHabitVm> _habitVms;

    private SolidColorBrush _firstColor;
    private SolidColorBrush _secondColor;

    private int _monthDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
    private List<Day> _mdList = new();

    public MainViewModel()
    {
        SetHabits();
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
            _habitVms = new SmartCollection<IHabitVm>();
            for (var i = 0; i < _habits.Count; i++)
            {
                var color = _firstColor;
                if (i % 2 != 0)
                    color = _secondColor;
                
                var habitVm = AutoFac.Default.Container.Resolve<IHabitVm>(
                    new TypedParameter(typeof(SolidColorBrush), color), new TypedParameter(typeof(int), 
                       _monthDays), new TypedParameter(typeof(Habit), _habits[i]));
                _habitVms.Add(habitVm);
            }

            return new ReadOnlyObservableCollection<IHabitVm>(_habitVms);
        }
    }
    
    public List<Day> MonthDays
    {
        get
        {
            for (var i = 0; i < _monthDays; i++)
            {
                _mdList.Add(new Day{Number = i+1});
            }
            return new List<Day>(_mdList);
        }
    }

    private async void SetHabits()
    {
        var service = AutoFac.Default.Container.Resolve<IHabitService>();

        var habitsResponse = await service.GetHabits();
        if(habitsResponse.IsError) return;

        _habits.Clear();
        _habits.AddRange(habitsResponse.Value.Habits);
        RaisePropertyChanged(nameof(Habits));
    }

    private void SetColors()
    {
        _firstColor = new SolidColorBrush((Color)Application.Current.Resources["MainColor"]);
        _secondColor = new SolidColorBrush((Color)Application.Current.Resources["AlternativeColor"]);
    }
}