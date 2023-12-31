using CashFlowAPI.DB;
using CashFlowAPI.Features.Auth;
using CashFlowAPI.Features.HealthCheck;
using CashFlowAPI.Features.ApprovedUsers;

var builder = WebApplication.CreateBuilder(args);
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
var audience = builder.Configuration["Auth0:Audience"];
builder.Services
    .AddApprovedUsersServices()
    .AddAuthServices(domain, audience)
    .AddDBServices();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
// app.UseCors("MyAllowedOrigins");
app
    .MapApprovedUsersEndpoints()
    .MapHealthEndpoints();
app.Run();
