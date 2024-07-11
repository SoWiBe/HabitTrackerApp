using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Autofac;
using Common.Entities;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Abstractions.Services;
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
    
    
    private Action<Habit?, bool> EventHabitDay => (habit, operation) =>
    {
        if(habit is null) return;
        if (_habit == null) return;

        if (operation) _habit.CountDays++;
        else _habit.CountDays--;
        
        _raiseEvent?.Invoke();
        RaisePropertyChanged(nameof(Habit));
    };

    private Action? _raiseEvent;
    public HabitViewModel(SolidColorBrush color, int monthDays, Habit? habit = null, Action raiseEvent = null)
    {
        _habit = habit;
        _raiseEvent = raiseEvent;
        _backgroundColor = color;
        _monthDays = monthDays;
        
        var createDayHabitsThread = new Thread(CreateDayHabits);
        createDayHabitsThread.Start();
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
            _dayHabitsVms = new SmartCollection<IHabitDayVm>();

            foreach (var dayHabit in _dayHabits)
            {
                var dh = AutoFac.Default.Container.Resolve<IHabitDayVm>(new TypedParameter(typeof(SolidColorBrush),
                    _backgroundColor), new TypedParameter(typeof(DayHabit), dayHabit), new TypedParameter(typeof(Action<Habit?, bool>), EventHabitDay));
                _dayHabitsVms.Add(dh);
            }
            
            return new ReadOnlyObservableCollection<IHabitDayVm>(_dayHabitsVms);
        }
    }

    private void CreateHabit()
    {
        MessageBox.Show(_habit.Title);
    }

    private void CreateDayHabits()
    {
        var monthDays = new List<DayHabit>();
        
        for(var i = 0; i < _monthDays; i++)
        {
            var dh = new DayHabit
            {
                Id = Guid.NewGuid(),
                Day = new Day { Number = 0 + 1 },
                Habit = _habit,
                IsComplete = false
            };

            monthDays.Add(dh);
        }
        
        _dayHabits.AddRange(monthDays);
        RaisePropertyChanged(nameof(HabitDays));
    }
}