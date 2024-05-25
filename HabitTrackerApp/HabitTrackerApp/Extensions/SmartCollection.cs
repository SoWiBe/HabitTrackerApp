using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HabitTrackerApp.Extensions;

public class SmartCollection<T> : ObservableCollection<T>
{
    private bool _suspendCollectionChangeNotification;

    public SmartCollection()
    {
        _suspendCollectionChangeNotification = false;
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!_suspendCollectionChangeNotification)
            base.OnCollectionChanged(e);
    }
        
    private void SuspendCollectionChangeNotification()
        => _suspendCollectionChangeNotification = true;

    private void ResumeCollectionChangeNotification()
        => _suspendCollectionChangeNotification = false;

    public void AddRange(IEnumerable<T> items)
    {
        SuspendCollectionChangeNotification();

        try
        {
            foreach (var i in items)
                base.InsertItem(Count, i);
        }
        finally
        {
            ResumeCollectionChangeNotification();
            var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arg);
        }
    }
    
    public bool TryAdd(T value)
    {
        if (Contains(value))
            return false;
        
        Add(value);
        return true;
    }

}