using Microsoft.AspNetCore.Mvc;
using 
namespace UberDinner.Api.Controllers;

public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<string> errors)
    {
        return BadRequest(new { Errors = errors });

    }
}