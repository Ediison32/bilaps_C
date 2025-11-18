using Microsoft.EntityFrameworkCore;
using webInmobiliary.Application.Interfaces;
using webInmobiliary.Application.Services;
using webInmobiliary.Domain.Interfaces;
using webInmobiliary.Infrastructure.Data;
using webInmobiliary.Infrastructure.Extensions;
using webInmobiliary.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Application Services
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILoginService, LoginService>();
// builder.Services.AddScoped<IPropertyService, PropertyService>();
// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
// builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
// Para el DbContext en LoginService
builder.Services.AddScoped<DbContext, AppDbContext>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS: permitir cualquier origen en entorno de desarrollo
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();