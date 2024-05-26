using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Autofac;
using Common.Entities;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Commands;
using HabitTrackerApp.Di;
using HabitTrackerApp.Extensions;
using HabitTrackerApp.UI.Views.Popups;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitViewModel : BaseObservableElementViewModel, IHabitVm
{
    private Habit? _habit;
    private SolidColorBrush _backgroundColor;
    private RelayCommand _createHabitCommand;

    private int _monthDays;

    private readonly List<DayHabit> _dayHabits = new();
    private SmartCollection<IHabitDayVm> _dayHabitsVms;
    
    private Action<Habit?> EventHabitDay => (habit) =>
    {
        if(habit is null)
            return;

        habit.CountDays++;
    };

    public HabitViewModel(SolidColorBrush color, int monthDays, Habit? habit = null)
    {
        _habit = habit;
        _backgroundColor = color;
        _monthDays = monthDays;
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

    public ReadOnlyObservableCollection<IHabitDayVm> HabitDays
    {
        get
        {
            CreateDayHabits();
            _dayHabitsVms = new SmartCollection<IHabitDayVm>();

            foreach (var dayHabit in _dayHabits)
            {
                var dh = AutoFac.Default.Container.Resolve<IHabitDayVm>(new TypedParameter(typeof(SolidColorBrush),
                    _backgroundColor));
                _dayHabitsVms.Add(dh);
            }
            
            return new ReadOnlyObservableCollection<IHabitDayVm>(_dayHabitsVms);
        }
    }

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

    private void CreateDayHabits()
    {
        for(var i = 0; i < _monthDays; i++)
        {
            var dh = new DayHabit
            {
                Day = new Day { Number = i + 1 },
                Habit = _habit,
                IsComplete = false
            };
            
            _dayHabits.Add(dh);
        }
    }
}