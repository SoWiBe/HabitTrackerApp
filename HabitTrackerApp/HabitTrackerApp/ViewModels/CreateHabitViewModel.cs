using HabitTrackerApp.Abstractions;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.ViewModels;

public class CreateHabitViewModel : BaseViewModel, ICreateHabitVm
{
    private string _title;
    
    public CreateHabitViewModel()
    {
        
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}