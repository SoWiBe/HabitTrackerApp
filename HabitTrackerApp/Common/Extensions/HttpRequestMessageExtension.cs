using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Common.Extensions;

public static class HttpRequestMessageExtension
{
    public static void SetJsonAsContent(this HttpRequestMessage request, object data)
    {
        var json = JsonSerializer.Serialize(data);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
    }

    public static void SetAuthorization(this HttpRequestMessage request, string? token)
    {
        if (token == null)
            return;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public static void SetObjectAsQueryValue(this HttpRequestMessage request, object data)
    {
        var query = GetQueryObject(data);

        if (string.IsNullOrEmpty(query))
            return;
        
        request.RequestUri = new Uri($"{request.RequestUri}{query}");
    }

    private static string GetQueryObject(object data)
    {
        var type = data.GetType();

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var propertyAttribute = typeof(FromQueryAttribute);
        
        var result = properties.Where(c => c.GetValue(data) != null)
            .Select(c =>
            {
                var attribute = c.GetCustomAttribute(propertyAttribute) as FromQueryAttribute;
                return $"{attribute?.Name ?? c.Name}={c.GetValue(data)}";
            }).ToList();

        return !result.Any() ? string.Empty : $"?{string.Join("&", result)}";
    }
}
