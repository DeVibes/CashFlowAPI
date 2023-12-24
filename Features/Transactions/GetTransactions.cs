using CashFlowAPI.Contracts.Requests;
using CashFlowAPI.Contracts.Responses;
using CashFlowAPI.Features.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowAPI.Features.Transactions;

public static class GetTransactions
{
    public static async Task<IResult> HandleGetTransactionsEndpoint([FromBody]GetTransactionsRequest request, GetTransactionsHandler handler, CancellationToken cancellationToken)
    {
        var query = new GetTransactionsQuery();
        var result = await handler.Handle(query, cancellationToken);
        var response = new GetTransactionsResponse(result);
        return Results.Ok(response);
    }
}

public record GetTransactionsQuery
{
    // filters
}

public record TransactionsReadModel(IEnumerable<TransactionReadModel> Transactions);

public class GetTransactionsHandler : IQueryHandler<GetTransactionsQuery, TransactionsReadModel>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<TransactionsReadModel> Handle(GetTransactionsQuery query, CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionsRepository.GetTransactions(query, cancellationToken);
        return transactions;
    }
}