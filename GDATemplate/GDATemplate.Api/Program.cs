
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GDATemplate.Api.Authorization;
using GDATemplate.Api.Extensions;
using GDATemplate.Application.Configuration;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;
ConfigurationManager configuration = builder.Configuration;
Assembly assembly = Assembly.GetExecutingAssembly();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(true);

builder.Logging.ClearProviders().AddConsole();

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleIOC()));

builder.Services.AddHealthChecks(configuration);
builder.Services.AddPolicies(configuration);
builder.Services.AddContexts(configuration);
builder.Services.AddMapping();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = configuration["LoginSP:Authority"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.FromMinutes(5),
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = true,
        };
    });

builder.Services.AddControllersWithViews(o => { o.Conventions.Add(new AddAuthorizeFiltersControllerConvention()); })
    .AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.EnableAnnotations();
    var teste = assembly.FullName.Split(",");
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = teste[0] });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "API TEMPLATE");
    });
}

app.UsePathBase(configuration["ContextoApp"]);

app.Use((context, next) =>
{
    context.Request.PathBase = configuration["ContextoApp"];
    return next();
});


app.UseHealthChecks("/health/status", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();