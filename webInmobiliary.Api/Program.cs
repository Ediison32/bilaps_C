using webInmobiliary.Application.Interfaces;
using webInmobiliary.Application.Services;
using webInmobiliary.Domain.Interfaces;
using webInmobiliary.Infrastructure.Extensions;
using webInmobiliary.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Database configuration
builder.Services.AddInfrastructure(builder.Configuration);

// Inyectar propier 
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();


// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// contruccion de los controladores 
builder.Services.AddControllers();

// construccion de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();