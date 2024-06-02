using Autofac;
using Common.Abstraction.Repositories;
using Common.Repositories;
using HabitTrackerApp.Abstractions.Services;
using HabitTrackerApp.Extensions;
using HabitTrackerApp.Repositories;
using HabitTrackerApp.Repositories.Core;
using HabitTrackerApp.Services;
using HabitTrackerApp.ViewModels;
using HabitTrackerApp.ViewModels.Core;

namespace HabitTrackerApp.Di;

public sealed class AutoFac
{
    private AutoFac()
    {
        Builder = new ContainerBuilder();
    }
    public static AutoFac Default { get; } = new();
    public ContainerBuilder Builder { get; }
    public IContainer Container { get; private set; }
    
    public void Build()
    {
        Builder.AddConfiguration();
        Builder.AddHttpClientFactory();
        
        Builder.RegisterType<GlobalSettings>().As<IGlobalSettings>();
        Builder.RegisterType<JsonRepository>().As<IJsonRepository>();
        Builder.RegisterType<ApiRepository>().As<IApiRepository>();
        
        Builder.RegisterType<HabitService>().As<IHabitService>();
        
        Builder.RegisterType<MainViewModel>().As<IMainVm>();
        Builder.RegisterType<HabitViewModel>().As<IHabitVm>();
        Builder.RegisterType<HabitDayViewModel>().As<IHabitDayVm>();
        Builder.RegisterType<CreateHabitViewModel>().As<ICreateHabitVm>();

        Container = Builder.Build();
    }

}