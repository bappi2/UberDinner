using UberDinner.Application;
using UberDinner.Infrastructure;
using UberDinner.Application.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
}

var app = builder.Build(); {
    // request pipeline
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}


