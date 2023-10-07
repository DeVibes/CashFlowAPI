using CashFlowAPI.Features.Common.Interfaces;
using CashFlowAPI.Repositories;

namespace CashFlowAPI.Features.Account;

public static class Setup
{
    public static IServiceCollection AddAccountServices(this IServiceCollection services)
    {
        services.AddSingleton<ITransactionsRepository, MongoTransactionsRepo>();
        services.AddSingleton<CreateTransactionHandler>();
        services.AddSingleton<GetTransactionsHandler>();
        services.AddSingleton<GetTransactionHandler>();
        services.AddSingleton<DeleteTransactionHandler>();
        return services;
    }

    public static WebApplication MapAccountEndpoints(this WebApplication application)
    {
        application.MapPost(ApiRoutes.Account.CreateTransaction, CreateTransaction.HandleCreateTransactionEndpoint);
        application.MapGet(ApiRoutes.Account.GetTransactions, GetTransactions.HandleGetTransactionsEndpoint);
        application.MapGet(ApiRoutes.Account.GetTransaction, GetTransaction.HandleGetTransactionEndpoint);
        application.MapDelete(ApiRoutes.Account.DeleteTransaction, DeleteTransaction.HandleDeleteTransactionEndpoint);
        return application;
    }
}