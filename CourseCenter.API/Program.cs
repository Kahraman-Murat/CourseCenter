using CourseCenter.API.Validators;
using CourseCenter.Business;
using CourseCenter.DataAccess;
using CourseCenter.DTO.DTOs.UserDtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = Assembly.GetExecutingAssembly();
var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);

//ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr-TR");

builder.Services
    .AddAutoMapper(assembly)
    .AddFluentValidationAutoValidation()
    .AddScoped<IValidator<UserRegisterDto>, UserRegisterValidator>()
    //.AddScoped<IValidator<LoginDto>, LoginValidator>()
    .AddEndpointsApiExplorer() // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseCenter", Version = "v1", Description = "CourseCenter Swagger client." });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "'Bearer' yazip bosluk birakip Token'i Girebilirsiniz \r\n\r\n Örnegin: \"Bearer P1ZbLCqpzOLie0eVKy1uRPFXywOdZUpxv8wkoJrR68LJEg8HriFtchrGLrpBgz8v\""
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {                        
                        Type=ReferenceType.SecurityScheme,                        
                        Id="Bearer"                    
                    }
                },                
                Array.Empty<string>()            
            }            
        });
    })
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
