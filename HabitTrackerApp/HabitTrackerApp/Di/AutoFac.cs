using Autofac;
using HabitTrackerApp.ViewModels;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.Di;

public class AutoFac
{
    public static AutoFac Default = new();

    private AutoFac()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        Container = builder.Build();
    }
    
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<MainViewModel>().As<IMainVm>();
        builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        
        return builder.Build();
    }
    
    public IContainer Container { get; }
}