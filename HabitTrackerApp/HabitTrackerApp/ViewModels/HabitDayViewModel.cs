using System.Windows;
using System.Windows.Media;
using HabitTrackerApp.Abstractions;
using HabitTrackerApp.Commands;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class HabitDayViewModel : BaseViewModel, IHabitDayVm
{
    private SolidColorBrush _solidColor;
    private string? _title;

    private RelayCommand _setHabitStatusCommand;
    
    public HabitDayViewModel(SolidColorBrush solidColor, string? title = null)
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
    
    public RelayCommand SetHabitStatusCommand => _setHabitStatusCommand ??= new RelayCommand(_ => SetHabitStatus());

    private void SetHabitStatus()
    {
        MessageBox.Show($"set status");
    }
}