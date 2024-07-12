using System;
using System.Windows;
using System.Windows.Media;
using Common.Entities;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Abstractions.Services;
using HabitTrackerApp.Commands;
using HabitTrackerApp.ViewModels.Core;
using HabitTrackerAppBackend.Endpoints.HabitsEndpoints;

namespace HabitTrackerApp.ViewModels;

public class HabitDayViewModel : BaseViewModel, IHabitDayVm
{
    private readonly IHabitService _habitService;
    private SolidColorBrush _solidColor;
    private readonly DayHabit? _dayHabit;
    private bool _isSuccess = false;

    private RelayCommand _setHabitStatusCommand;
    private Action<Habit?, bool> _eventHabit;

    public HabitDayViewModel(IHabitService habitService, SolidColorBrush solidColor, DayHabit? dayHabit = null, Action<Habit?, bool> eventHabit = null)
    {
        _habitService = habitService;
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

    private async void SetHabitStatus()
    {
        if (_isSuccess)
        {
            _isSuccess = !_isSuccess;
            var resultStatus = await _habitService.PostDayStatus(new PostDayStatusRequest
            {
                Title = _dayHabit.Habit.Title,
                Number = _dayHabit.Day.Number,
                IsComplete = _isSuccess
            });
            if(resultStatus.IsError) return;
            
            _eventHabit.Invoke(_dayHabit.Habit, false);
            RaisePropertyChanged(nameof(VisibilitySuccess));
            return;
        }
        
        _isSuccess = !_isSuccess;
        var result = await _habitService.PostDayStatus(new PostDayStatusRequest
        {
            Title = _dayHabit.Habit.Title,
            Number = _dayHabit.Day.Number,
            IsComplete = _isSuccess
        });
        if(result.IsError) return;
        
        _eventHabit.Invoke(_dayHabit.Habit, true);
        RaisePropertyChanged(nameof (VisibilitySuccess));
    }

    private void SetDayHabit()
    {
        if (_dayHabit is not null)
            _isSuccess = _dayHabit.IsComplete;
        
        RaisePropertyChanged(nameof(IsSuccess));
    }
}