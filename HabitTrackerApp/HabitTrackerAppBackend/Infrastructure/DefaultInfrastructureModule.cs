using System.Reflection;
using HabitTrackerAppBackend.Infrastructure.Errors;
using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.Authentication;
using Module = Autofac.Module;

namespace HabitTrackerAppBackend.Infrastructure;

public class DefaultInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommonDependencies(builder);
    }

    private void RegisterCommonDependencies(ContainerBuilder builder)
    {
        builder.RegisterType<ExceptionHandlerMiddleware>().AsSelf().InstancePerLifetimeScope();

        var module = new ConfigurationModule(GetConfiguration());
        builder.RegisterModule(module);
    }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

        builder.AddJsonFile("appsettings.json", true, true);

#if DEBUG
        builder.AddJsonFile("appsettings.Development.json", true, true);
#endif

        return builder.Build();
    }
}