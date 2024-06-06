using Microsoft.AspNetCore.Mvc.Infrastructure;
using UberDinner.Api.Errors;
using UberDinner.Api.Filters;
using UberDinner.Api.Middleware;
using UberDinner.Application;
using UberDinner.Infrastructure;
using UberDinner.Application.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, UberDinnerProblemDetailsFactory>();
}

var app = builder.Build(); {
    // request pipeline
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}


