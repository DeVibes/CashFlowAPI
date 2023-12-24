using CashFlowAPI.Features.Common.Interfaces;

namespace CashFlowAPI.Features.Transactions;

public static class DeleteTransaction
{
    public static async Task<IResult> HandleDeleteTransactionEndpoint(Guid id, DeleteTransactionHandler handler, CancellationToken cancellationToken = default)
    {
        var command = new DeleteTransactionCommand(id);
        await handler.Handle(command, cancellationToken);
        return Results.NoContent();
    }
}

public record DeleteTransactionCommand(Guid Id);

public class DeleteTransactionHandler : ICommandHandler<DeleteTransactionCommand>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public DeleteTransactionHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task Handle(DeleteTransactionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _transactionsRepository.DeleteTransaction(command, cancellationToken);
    }
}
