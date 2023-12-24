using CashFlowAPI.Features.Common.Interfaces;
using CashFlowAPI.Repositories;

namespace CashFlowAPI.Features.Transactions;

public static class Setup
{
    public static IServiceCollection AddTransactionsServices(this IServiceCollection services)
    {
        services.AddSingleton<ITransactionsRepository, MongoTransactionsRepo>();
        services.AddSingleton<CreateTransactionHandler>();
        services.AddSingleton<GetTransactionsHandler>();
        services.AddSingleton<GetTransactionHandler>();
        services.AddSingleton<DeleteTransactionHandler>();
        return services;
    }

    public static WebApplication MapTransactionsEndpoints(this WebApplication application)
    {
        application.MapPost(ApiRoutes.Transactions.CreateTransaction, CreateTransaction.HandleCreateTransactionEndpoint);
        application.MapGet(ApiRoutes.Transactions.GetTransactions, GetTransactions.HandleGetTransactionsEndpoint);
        application.MapGet(ApiRoutes.Transactions.GetTransaction, GetTransaction.HandleGetTransactionEndpoint);
        application.MapDelete(ApiRoutes.Transactions.DeleteTransaction, DeleteTransaction.HandleDeleteTransactionEndpoint);
        return application;
    }
}