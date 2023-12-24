namespace CashFlowAPI.Features.HealthCheck;

public static class IsAlive
{
    public static async Task<IResult> HandleIsAliveEndpoint()
    {
        return Results.Ok("Hello!");
    }

    public static async Task<IResult> HandleNoScopeEndpoint()
    {
        return Results.Ok("Hello! from auth");
    }
    
    public static async Task<IResult> HandleUserScopeEndpoint()
    {
        return Results.Ok("Hello! from user scope");
    }

    public static async Task<IResult> HandleAdminScopeEndpoint()
    {
        return Results.Ok("Hello! from admin scope");
    }
}