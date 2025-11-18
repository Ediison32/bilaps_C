using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webInmobiliary.Domain.Interfaces;
using webInmobiliary.Infrastructure.Data;
using webInmobiliary.Infrastructure.Repositories;

namespace webInmobiliary.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );
        
        services.AddJwtAuthentication(configuration);

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? 
                       Environment.GetEnvironmentVariable("JWT_SECRET") ?? 
                       "default-secret-key-min-32-chars-long-here";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"] ?? "webInmobiliary",
                ValidAudience = jwtSettings["ValidAudience"] ?? "webInmobiliary-users",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
    
    // public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var cloudName = configuration["Cloudinary:CloudName"];
    //     var apiKey = configuration["Cloudinary:ApiKey"];
    //     var apiSecret = configuration["Cloudinary:ApiSecret"];
    //
    //     var account = new Account(cloudName, apiKey, apiSecret);
    //     var cloudinary = new Cloudinary(account)
    //     {
    //         Api = { Secure = true }
    //     };
    //
    //     services.AddSingleton(cloudinary);
    //     services.AddScoped<ICloudinaryRepository, CloudinaryRepositories>();
    //
    //     return services;
    // }
}