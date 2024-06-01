namespace Common.Entities.Core.Error;

public interface IErrorOr
{
    List<Errors.Error>? Errors { get; }
    bool IsError { get; }
}