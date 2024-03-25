using UberDinner.Domain.Entities;

namespace UberDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}