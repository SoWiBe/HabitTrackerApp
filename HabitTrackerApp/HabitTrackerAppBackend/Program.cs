using System.Text.Json.Serialization;
using Autofac;
using HabitTrackerAppBackend.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
const string version = "v0.0.1";
const string swaggerUrl = $"/swagger/{version}/swagger.json";
const string swaggerName = "CyberClub Admin API";

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddCors();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configBuilder = new ConfigurationBuilder();

configBuilder.AddJsonFile("appsettings.json", false, true);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();