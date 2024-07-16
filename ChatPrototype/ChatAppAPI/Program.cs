using AutoMapper;
using ChatAppAPI.Handlers;
using ChatAppAPI.Handlers.Requirements;
using ChatAppAPI.Helpers;
using ChatAppAPI.Services;
using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ChatAppAPI.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = configBuilder.Build();

// Create service collection
ServiceCollection services = new ServiceCollection();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMappers();

builder.Services.AddDbContext<ChatAppDBContext>(options => options.UseSqlServer(configuration.GetSection("ConnectionStrings")["ChatAppConnectionString"]));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<IChannelService, ChannelService>();
builder.Services.AddSingleton<IPusherService, PusherService>(serviceProvider =>
{
    return new PusherService(
        configuration.GetSection("PusherServiceConnections")["AppId"],
        configuration.GetSection("PusherServiceConnections")["Key"],
        configuration.GetSection("PusherServiceConnections")["Cluster"]
    );
});
builder.Services.AddScoped<IAuthorizationHandler, TokenHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Token", policy =>
        policy.Requirements.Add(new TokenRequirement()));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatAPI", Version = "v1" });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    c.OperationFilter<AuthenticationRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
