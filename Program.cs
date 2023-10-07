using CashFlowAPI.DB;
using CashFlowAPI.Features.Account;
using CashFlowAPI.Features.HealthCheck;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAccountServices()
    .AddDBServices();
var app = builder.Build();

app
    .MapAccountEndpoints()
    .MapHealthEndpoints();

app.Run();
