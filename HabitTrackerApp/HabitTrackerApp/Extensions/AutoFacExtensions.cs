using System.IO;
using System.Net.Http;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HabitTrackerApp.Extensions;

public static class AutoFacExtensions
{
    public static void AddConfiguration(this ContainerBuilder builder, string? configPath = null)
    {
        builder.Register(_ =>
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(string.IsNullOrEmpty(configPath) ? "appsettings.json" : configPath);

            return configBuilder.Build();
        }).AsSelf().As<IConfiguration>().SingleInstance();
    }

    public static void AddHttpClientFactory(this ContainerBuilder builder)
    {
        builder.Register(_ =>
        {
            var services = new ServiceCollection();
            services.AddHttpClient(Options.DefaultName, c =>
            {
                // ...
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, certChain, policyErrors) => true
                };
            });
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IHttpClientFactory>();
        }).AsSelf().As<IHttpClientFactory>();
    }
}