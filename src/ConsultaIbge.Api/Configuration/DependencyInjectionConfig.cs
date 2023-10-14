using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Application.Services;
using ConsultaIbge.Data.Context;
using ConsultaIbge.Data.Repositories;
using ConsultaIbge.Domain.Interfaces;

namespace ConsultaIbge.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Data
        services.AddScoped<IIbgeRepository, IbgeRepository>();
        services.AddScoped<ApplicationContext>();

        // Service
        services.AddScoped<IIbgeService, IbgeService>();
    }
}
