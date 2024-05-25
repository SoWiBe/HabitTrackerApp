using Autofac;
using HabitTrackerApp.ViewModels;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.Di;

public class AutoFac
{
    public static AutoFac Default = new();
    
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<MainViewModel>().As<IMainVm>();
        builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        
        return builder.Build();
    }
    
    public IContainer Container { get; }
}