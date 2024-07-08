using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonsServices();

var app = builder.Build();

app.UserCommonsMiddlewares();

app.Run();

