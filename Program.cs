using CashFlowAPI.DB;
using CashFlowAPI.Features.Account;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAccountServices()
    .AddDBServices();
var app = builder.Build();

app.MapAccountEndpoints();

app.Run();
