using Autofac;
using Common.Abstraction.Repositories;
using Common.Repositories;
using HabitTrackerApp.Repositories;
using HabitTrackerApp.Repositories.Core;
using HabitTrackerApp.ViewModels;
using HabitTrackerApp.ViewModels.Core;
using HabitTrackerAppBackend.Extensions;

namespace HabitTrackerApp.Di;

public class AutoFac
{
    public static AutoFac Default = new();

    private AutoFac()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        builder.RegisterType<HabitDayViewModel>().As<IHabitDayVm>();
        Container = builder.Build();
    }
    
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();
        
        builder.AddConfiguration();
        builder.AddHttpClientFactory();
        builder.RegisterType<MainViewModel>().As<IMainVm>();
        builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        builder.RegisterType<CreateHabitViewModel>().As<ICreateHabitVm>();
        builder.RegisterType<GlobalSettings>().As<IGlobalSettings>();
        builder.RegisterType<JsonRepository>().As<IJsonRepository>();
        builder.RegisterType<ApiRepository>().As<IApiRepository>();
        
        return builder.Build();
    }
    
    public IContainer Container { get; }
}