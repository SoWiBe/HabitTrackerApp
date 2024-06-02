using Common.Entities.Errors;

namespace Common.Abstraction.Repositories;

public interface IApiRepository
{
    Task<ErrorOr<HttpResponseMessage>> PostDataWithResponseAsync(string url, object data, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> GetResponseAsync(string url, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> GetResponseWithDataAsync(string url, object data, string? token = null);
    Task<ErrorOr<Stream>> GetStreamResponseAsync(string url, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> PutDataWithResponseAsync(string url, object data, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> PutWithResponseAsync(string url, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> DeleteResponseWithDataAsync(string url, object data, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> DeleteResponseAsync(string url, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> PatchDataWithResponseAsync(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PostDataWithResponseAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PostQueryWithResponseAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PostWithResponseAsync<TResponse>(string url, string? token = null);
    Task<ErrorOr<TResponse?>> GetResponseAsync<TResponse>(string url, string? token = null);
    Task<ErrorOr<TResponse?>> DeleteResponseAsync<TResponse>(string url, string? token = null);
    Task<ErrorOr<TResponse?>> GetResponseWithDataAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PutDataWithResponseAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PutWithResponseAsync<TResponse>(string url, string? token = null);
    Task<ErrorOr<TResponse?>> DeleteResponseWithDataAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<TResponse?>> PatchDataWithResponseAsync<TResponse>(string url, object data, string? token = null);
    Task<ErrorOr<HttpResponseMessage>> PatchAsync(string url, string? token = null);

}