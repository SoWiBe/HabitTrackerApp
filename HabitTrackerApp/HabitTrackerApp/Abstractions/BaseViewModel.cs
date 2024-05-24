using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HabitTrackerApp.Abstractions.Core;

namespace HabitTrackerApp.Abstractions;

public class BaseViewModel : IBaseViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void RaisePropertyChanging([CallerMemberName] string propertyName = "")
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    public virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
        RaisePropertyChanging(propertyName);
        storage = value;
        RaisePropertyChanged(propertyName);
        return true;
    }

}