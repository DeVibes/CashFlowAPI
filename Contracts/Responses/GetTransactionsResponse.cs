using CashFlowAPI.Features.Account;

namespace CashFlowAPI.Contracts.Responses;

public record GetTransactionsResponse
{
    public GetTransactionsResponse(TransactionsReadModel readModel)
    {
        Payload = readModel.Transactions.Select(x => new GetTransactionResponse(x));
    }
    public IEnumerable<GetTransactionResponse> Payload { get; set; }
}