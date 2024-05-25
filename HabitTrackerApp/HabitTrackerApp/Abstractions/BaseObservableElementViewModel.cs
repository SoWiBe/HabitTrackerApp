using System;
using HabitTrackerApp.Abstractions.Core;

namespace HabitTrackerApp.Abstractions;

public abstract class BaseObservableElementViewModel : BaseViewModel, IBaseObservableElementViewModel
{
    public BaseObservableElementViewModel()
    {
        Loading += LoadingEvent;
    }

    public Action Loading { get; set; }
    protected abstract void LoadingEvent();
}