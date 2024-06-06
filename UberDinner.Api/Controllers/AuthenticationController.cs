using Microsoft.AspNetCore.Mvc;
using UberDinner.Api.Filters;
using UberDinner.Application.Services.Authentication;
using UberDinner.Contracts.Authentication;

namespace UberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController: ControllerBase
{
    //private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [Route("register")]
    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        //Console.WriteLine(request.ToString());
        var authenticationResult = _authenticationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);
        var AuthenticationResponse = new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
        return Ok(AuthenticationResponse);
    }
    
    [Route("login")]
    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        var authenticationResult = _authenticationService.Login(request.Email, request.Password);
        var authenticationResponse = new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
        return Ok(authenticationResponse);
    }
}