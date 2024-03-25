using UberDinner.Application.Common.Interfaces.Authentication;
using UberDinner.Application.Common.Interfaces.Persistence;
using UberDinner.Domain.Entities;

namespace UberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator; 
    private readonly IUserRepository _userRepository;
    
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }   
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // check if the user exists in the database
        if (_userRepository.GetUserByEmail(email) != null)
        {
            throw new Exception("User already exists");
        }   
        // create a new user in the database and generate a unique id 
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password // this should be hashed
        };
        _userRepository.Add(user);
        
        // create jwt token and return it
        //Guid userid = user.Id;
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. check if the user exists in the database
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User does not exist");
        }
        // 2. check if the password is correct
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }
        // 3. generate a jwt token and return it
        //var user = _userRepository.GetUserByEmail(email);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}