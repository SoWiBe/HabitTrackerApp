using Autofac;
using HabitTrackerApp.ViewModels;

namespace HabitTrackerApp.Di;

public class AutoFac
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<MainViewModel>().AsSelf();
        return builder.Build();
    }
}