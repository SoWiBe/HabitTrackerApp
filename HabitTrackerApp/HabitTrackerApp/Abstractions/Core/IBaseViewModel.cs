using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HabitTrackerApp.Abstractions.Core;

public interface IBaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging
{
    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "");
}