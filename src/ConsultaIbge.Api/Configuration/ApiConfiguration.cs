using ConsultaIbge.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Api.Configuration;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));

        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseCors("Total");
    }
}
