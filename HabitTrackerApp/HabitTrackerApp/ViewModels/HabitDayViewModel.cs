using System;
using System.Windows;
using System.Windows.Media;
using Common.Entities;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Commands;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitDayViewModel : BaseViewModel, IHabitDayVm
{
    private SolidColorBrush _solidColor;
    private readonly DayHabit? _dayHabit;
    private bool _isSuccess = false;

    private RelayCommand _setHabitStatusCommand;
    private Action<Habit?, bool> _eventHabit;

    public HabitDayViewModel(SolidColorBrush solidColor, DayHabit? dayHabit = null, Action<Habit?, bool> eventHabit = null)
    {
        _solidColor = solidColor;
        _dayHabit = dayHabit;
        _eventHabit = eventHabit;
        SetDayHabit();
    }
    
    public SolidColorBrush BackgroundColor
    {
        get => _solidColor;
        set => SetProperty(ref _solidColor, value);
    }

    public bool IsSuccess
    {
        get => _isSuccess;
        set => SetProperty(ref _isSuccess, value);
    }

    public DayHabit DayHabit { get; set; }

    public Visibility VisibilitySuccess => _isSuccess ? Visibility.Visible : Visibility.Hidden;

    public RelayCommand SetHabitStatusCommand => _setHabitStatusCommand ??= new RelayCommand(_ => SetHabitStatus());

    private void SetHabitStatus()
    {
        if (_isSuccess)
        {
            _isSuccess = !_isSuccess;
            _eventHabit.Invoke(null, false);
            RaisePropertyChanged(nameof(VisibilitySuccess));
            return;
        }
        
        _isSuccess = !_isSuccess;
        
        _eventHabit.Invoke(_dayHabit.Habit, true);
        
        _isSuccess = !_isSuccess;
        RaisePropertyChanged(nameof (VisibilitySuccess));
    }

    private void SetDayHabit()
    {
        if (_dayHabit is not null)
            _isSuccess = _dayHabit.IsComplete;
        
        RaisePropertyChanged(nameof(IsSuccess));
    }
}