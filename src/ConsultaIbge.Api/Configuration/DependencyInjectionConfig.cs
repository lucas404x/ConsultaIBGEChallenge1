using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Application.Services;
using ConsultaIbge.Application.Validators;
using ConsultaIbge.Data.Context;
using ConsultaIbge.Data.Repositories;
using ConsultaIbge.Domain.Interfaces;
using FluentValidation;

namespace ConsultaIbge.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Data
        services.AddScoped<ApplicationContext>();
        services.AddScoped<IIbgeRepository, IbgeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Service
        services.AddScoped<IIbgeService, IbgeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IPasswordHasherService, PasswordHasherService>();
        services.AddTransient<ITokenService, TokenService>();

        // Fluent
        services.AddValidatorsFromAssemblyContaining<IbgeAddValidation>();
    }
}
