using UberDinner.Application.Common.Interfaces.Persistence;
using UberDinner.Domain.Entities;

namespace UberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }
}