using Common.Abstraction.Repositories;
using Common.Entities.Errors;
using Common.Extensions;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Common.Repositories;

public class ApiRepository : IApiRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonRepository _jsonRepository;

    public ApiRepository(IHttpClientFactory httpClientFactory, IJsonRepository jsonRepository)
    {
        _httpClientFactory = httpClientFactory;
        _jsonRepository = jsonRepository;
    }

    public async Task<ErrorOr<HttpResponseMessage>> PostDataWithResponseAsync(string url, object data, string? token)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    public async Task<ErrorOr<HttpResponseMessage>> GetResponseAsync(string url, string? token)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    public async Task<ErrorOr<HttpResponseMessage>> GetResponseWithDataAsync(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.SetObjectAsQueryValue(data);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }
    
    public async Task<ErrorOr<TResponse?>> DeleteResponseAsync<TResponse>(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }


    public async Task<ErrorOr<Stream>> GetStreamResponseAsync(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.SetAuthorization(token);

        var result = await SendWithStreamResponse(client, request);

        return result;
    }

    public async Task<ErrorOr<HttpResponseMessage>> PutDataWithResponseAsync(string url, object data, string? token)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }
    
    public async Task<ErrorOr<HttpResponseMessage>> PutWithResponseAsync(string url, string? token)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    public async Task<ErrorOr<HttpResponseMessage>> DeleteResponseWithDataAsync(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }
    
    public async Task<ErrorOr<HttpResponseMessage>> DeleteResponseAsync(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    public async Task<ErrorOr<HttpResponseMessage>> PatchDataWithResponseAsync(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Patch, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    public async Task<ErrorOr<TResponse?>> PostDataWithResponseAsync<TResponse>(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> PostQueryWithResponseAsync<TResponse>(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.SetObjectAsQueryValue(data);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> PostDataAndQueryWithResponseAsync<TResponse>(string url, object data, object query, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.SetJsonAsContent(data);
        request.SetObjectAsQueryValue(query);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }
    
    public async Task<ErrorOr<TResponse?>> PostWithResponseAsync<TResponse>(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> GetResponseAsync<TResponse>(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> GetResponseWithDataAsync<TResponse>(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.SetObjectAsQueryValue(data);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }
    
    public async Task<ErrorOr<TResponse?>> PutDataWithResponseAsync<TResponse>(string url, object data, string? token)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);
        
        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> PutWithResponseAsync<TResponse>(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.SetAuthorization(token);
        
        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> DeleteResponseWithDataAsync<TResponse>(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);

        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<TResponse?>> PatchDataWithResponseAsync<TResponse>(string url, object data, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Patch, url);
        request.SetJsonAsContent(data);
        request.SetAuthorization(token);
        
        return await SendWithResponse<TResponse>(client, request);
    }

    public async Task<ErrorOr<HttpResponseMessage>> PatchAsync(string url, string? token = null)
    {
        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Patch, url);
        request.SetAuthorization(token);
        
        var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        return result;
    }

    private async Task<ErrorOr<TResponse?>> SendWithResponse<TResponse>(HttpClient client, HttpRequestMessage request)
    {
        try
        {
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            var res = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) 
                return Error.Unexpected(ConvertCode(res), Convert(res));
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = await _jsonRepository.Deserialize<TResponse>(responseContent);
            
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Error.Unexpected("Api.Unexpected", e.ToString());
        }
    }
    
    private async Task<ErrorOr<Stream>> SendWithStreamResponse(HttpClient client, HttpRequestMessage request)
    {
        try
        {
            var response = await client.SendAsync(request);
            var res = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) 
                return Error.Unexpected(ConvertCode(res), Convert(res));
            
            var responseContent = await response.Content.ReadAsStreamAsync();
            
            return responseContent;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Error.Unexpected("Api.Unexpected", e.ToString());
        }
    }

    private string Convert(string response)
    {
        try
        {
            var json = JsonConvert.DeserializeObject<JObject>(response);
            var description =
                (json?.SelectToken("detail[0].msg") ?? json?.SelectToken("detail") ?? json?.SelectToken("description"))
                ?.ToString();

            return string.IsNullOrEmpty(description) ? "Unexpected error" : description;
        }
        catch (Exception)
        {
            return "Unexpected error";
        }
    }
    
    private string ConvertCode(string response)
    {
        try
        {
            var json = JsonConvert.DeserializeObject<JObject>(response);
            var code = json?.SelectToken("code")?.ToString();

            return string.IsNullOrEmpty(code) ? "Api.Unexpected" : code;
        }
        catch (Exception)
        {
            return "Api.Unexpected";
        }
    }
}
