namespace CashFlowAPI.Features.HealthCheck;
public static class Setup {
    // public static IServiceCollection AddHealthServices(this IServiceCollection services)
    // {
    //     return services;
    // }

    public static WebApplication MapHealthEndpoints(this WebApplication application)
    {
        application.MapGet(ApiRoutes.Infra.IsAlive, IsAlive.HandleIsAliveEndpoint);
        return application;
    }
}