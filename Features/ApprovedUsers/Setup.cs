using CashFlowAPI.Features.Common.Interfaces;
using CashFlowAPI.Repositories;

namespace CashFlowAPI.Features.ApprovedUsers;

public static class Setup
{
    public static IServiceCollection AddApprovedUsersServices(this IServiceCollection services)
    {
        services.AddSingleton<IApprovedUsersRepository, MongoApprovedUsersRepo>();
        services.AddSingleton<ApprovedUsersCommandHandler>();
        services.AddSingleton<ApprovedUsersQueryHandler>();
        return services;
    }

    public static WebApplication MapApprovedUsersEndpoints(this WebApplication application)
    {
        application.MapPost(ApiRoutes.ApprovedUsers.ApproveUser, ApproveUserEndpoint.Handle);
        application.MapDelete(ApiRoutes.ApprovedUsers.DisapproveUser, DisapproveUserEndpoint.Handle);
        application.MapGet(ApiRoutes.ApprovedUsers.GetApprovedUsers, GetApprovedUsersEndpoint.Handle);
        application.MapGet(ApiRoutes.ApprovedUsers.GetApprovedUser, GetApprovedUserEndpoint.Handle);
        return application;
    }
}