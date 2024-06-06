using Microsoft.AspNetCore.Mvc;

namespace UberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.Message, statusCode: 500);
    } 
    
}