using CourseCenter.API.Validators;
using CourseCenter.Business;
using CourseCenter.DataAccess;
using CourseCenter.DTO.DTOs.AuthDtos;
using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.DTO.DTOs.UserDtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
//using System.Text.Json.Serialization;


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

// CORS Policy for all request
//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()
//));

// CORS Policy for one address
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        builder => builder.WithOrigins("http://localhost:4200")
//                          .AllowAnyMethod()
//                          .AllowAnyHeader());
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("https://localhost:7102") // Frontend URL'si
//              .AllowCredentials() // Cookie gönderimini etkinleştirir
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
        builder.WithOrigins("https://localhost:7102")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod());
});

builder.Services
    .AddAutoMapper(assembly)
    .AddFluentValidationAutoValidation()
    .AddScoped<IValidator<CreateUserDto>, CreateUserValidator>()
    .AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>()
    .AddScoped<IValidator<RequestLoginDto>, RequestLoginValidator>()
    .AddScoped<IValidator<CreateRoleDto>, CreateRoleValidator>()
    .AddScoped<IValidator<UpdateRoleDto>, UpdateRoleValidator>()
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
    //.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// for CORS
//app.UseCors();
//app.UseCors("AllowAngularApp");
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
