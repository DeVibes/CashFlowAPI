using CashFlowAPI.Features.Account;

namespace CashFlowAPI.Features.Common.Interfaces;

public interface ITransactionsRepository
{
    public Task<TransactionsReadModel> GetTransactions(GetTransactionsQuery query, CancellationToken cancellationToken = default);
    public Task<TransactionReadModel> GetTransaction(GetTransactionQuery query, CancellationToken cancellationToken = default);
    public Task<bool> CreateTransaction(CreateTransactionCommand command, CancellationToken cancellationToken = default);
    public Task<bool> DeleteTransaction(DeleteTransactionCommand command, CancellationToken cancellationToken = default);
}