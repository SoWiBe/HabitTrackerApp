namespace Common.Abstraction.Repositories;

public interface IJsonRepository
{
    Task<TResult?> Deserialize<TResult>(string json);
    Task<string> Serialize(object data);
}