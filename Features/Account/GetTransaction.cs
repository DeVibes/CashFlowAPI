using CashFlowAPI.Contracts.Responses;
using CashFlowAPI.Features.Common.Interfaces;

namespace CashFlowAPI.Features.Account;

public static class GetTransaction
{
    public static async Task<IResult> HandleGetTransactionEndpoint(Guid id, GetTransactionHandler handler, CancellationToken cancellationToken = default)
    {
        var query = new GetTransactionQuery(id);
        var result = await handler.Handle(query, cancellationToken);
        var response = new GetTransactionResponse(result);
        return Results.Ok(response);
    }
}

public record GetTransactionQuery(Guid Id);

public record TransactionReadModel
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
}

public class GetTransactionHandler : IQueryHandler<GetTransactionQuery, TransactionReadModel>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<TransactionReadModel> Handle(GetTransactionQuery query, CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionsRepository.GetTransaction(query, cancellationToken);
        return transaction;
    }
}
