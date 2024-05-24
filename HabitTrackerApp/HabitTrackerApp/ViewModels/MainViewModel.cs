using HabitTrackerApp.Abstractions;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class MainViewModel : BaseViewModel, IMainVm
{
    private string _title = "test";
    
    public MainViewModel()
    {
        
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}