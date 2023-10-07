namespace CashFlowAPI.Features.HealthCheck;

public static class IsAlive
{
    public static async Task<IResult> HandleIsAliveEndpoint()
    {
        return Results.Ok("Hello!");
    }
}