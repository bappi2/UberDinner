using Microsoft.Extensions.Configuration;
using UberDinner.Application.Common.Interfaces.Authentication;
using UberDinner.Application.Common.Interfaces.Persistence;
using UberDinner.Application.Services;
using UberDinner.Infrastructure.Authentication;
using UberDinner.Infrastructure.Persistence;
using UberDinner.Infrastructure.Services;

namespace UberDinner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}