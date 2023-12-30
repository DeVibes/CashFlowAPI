using CashFlowAPI.Common.Interfaces;
using CashFlowAPI.Repositories;

namespace CashFlowAPI.Features.ApprovedUsers;

public static class Setup
{
    public static IServiceCollection AddApprovedUsersServices(this IServiceCollection services)
    {
        services.AddSingleton<IApprovedUsersRepository, MongoApprovedUsersRepo>();
        services.AddSingleton<GetApprovedUserQueryHandler>();
        services.AddSingleton<GetApprovedUsersQueryHandler>();
        services.AddSingleton<ApproveUserCommandHandler>();
        services.AddSingleton<DisapproveUserCommandHandler>();
        return services;
    }

    public static WebApplication MapApprovedUsersEndpoints(this WebApplication application)
    {
        application
            .MapGet(ApiRoutes.ApprovedUsers.GetApprovedUsers, GetApprovedUsersEndpoint.Handle);
        application
            .MapGet(ApiRoutes.ApprovedUsers.GetApprovedUser, GetApprovedUserEndpoint.Handle)
            .WithName(nameof(GetApprovedUserEndpoint));
        application.MapPost(ApiRoutes.ApprovedUsers.ApproveUser, ApproveUserEndpoint.Handle);
        application.MapDelete(ApiRoutes.ApprovedUsers.DisapproveUser, DisapproveUserEndpoint.Handle);
        return application;
    }
}