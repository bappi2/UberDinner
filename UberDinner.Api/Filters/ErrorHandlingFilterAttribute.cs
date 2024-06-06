using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UberDinner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute   
{
    /*public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        context.Result  = new ObjectResult(new { error = exception.Message })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    } */
    
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var problemDetails = new ProblemDetails
        {
            Title = "An error occured while processing your request.",
            Instance =  context.HttpContext.Request.Path,
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
        context.ExceptionHandled = true;
    }
    
    
    
}