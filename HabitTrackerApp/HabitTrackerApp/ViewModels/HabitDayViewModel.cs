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
    private readonly DayHabit _dayHabit;
    private string? _title;
    private bool _isSuccess = false;

    private RelayCommand _setHabitStatusCommand;
    private Action<Habit?> _eventHabit;

    public HabitDayViewModel(SolidColorBrush solidColor,string? title = null)
    {
        _solidColor = solidColor;
        _title = title;
    }

    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
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
        /*
         * if (_isSuccess)
        {
            _isSuccess = !_isSuccess;
            _eventHabit.Invoke(null);
            RaisePropertyChanged(nameof(VisibilitySuccess));
            return;
        }
        
        _isSuccess = !_isSuccess;
        _eventHabit.Invoke(_dayHabit.Habit);
        RaisePropertyChanged(nameof(VisibilitySuccess));
         */

        _isSuccess = !_isSuccess;
        RaisePropertyChanged(nameof(VisibilitySuccess));
    }
}