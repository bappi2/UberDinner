namespace UberDinner.Application.Services;

public interface IDateTimeProvider
{
    DateTime    UtcNow { get; }
}