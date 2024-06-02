using HabitTrackerApp.Repositories.Core;
using Microsoft.Extensions.Configuration;

namespace HabitTrackerApp.Repositories;

public class GlobalSettings : IGlobalSettings
{
    private readonly IConfiguration _config;

    public GlobalSettings(IConfiguration config)
    {   
        _config = config;
    }
    
    public string Language => _config.GetValue<string>(Fields.Language) ?? "ru";
    public string ApiUrl => _config.GetValue<string>(Fields.ApiUrl) ?? "";

    private static class Fields
    {
        public const string Language = "language";
        public const string ApiUrl = "api_url";
    }

}