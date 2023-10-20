using ConsultaIbge.Core.Authentication;
using ConsultaIbge.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConsultaIbge.Api.Configuration;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration.GetValue<string>("Auth:PrivateKey") ?? throw new InvalidOperationException("JWT Private key not found.");
        JwtHelper.LoadFromSettings(jwtKey);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            //x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(JwtHelper.PrivateKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

        });
        services.AddAuthorization();

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

    public static void UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.UseHttpsRedirection();

        app.UseCors("Total");
    }
}
