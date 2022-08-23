using CopenhagenSoftware.Api;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

// AddControllers is needed so that MapControllers can handle attribute routing
// the controllers will still be created by our custom ControllerActivator
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IControllerActivator, ControllerActivator>();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.MapHealthChecks("/healthz");
app.Run();

// Make this class public so we don't need to expose internals to test
public partial class Program { }
