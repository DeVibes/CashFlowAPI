using CashFlowAPI.Contracts.Requests;
using CashFlowAPI.Features.Common.Interfaces;

namespace CashFlowAPI.Features.Account;

public static class CreateTransaction
{
    public static async Task<IResult> HandleCreateTransactionEndpoint(CreateTransactionRequest request, CreateTransactionHandler handler, CancellationToken cancellationToken = default)
    {
        var command = new CreateTransactionCommand
        {
            Price = request.Price,
            Description = request.Description
        };
        
        await handler.Handle(command, cancellationToken);

        return Results.Ok();
    }
}

public record CreateTransactionCommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Price { get; set; }
    public string Description { get; set; }
}

public class CreateTransactionHandler : ICommandHandler<CreateTransactionCommand>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public CreateTransactionHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task Handle(CreateTransactionCommand command, CancellationToken cancellationToken = default)
    {
        // TODO Validate command
        await _transactionsRepository.CreateTransaction(command, cancellationToken);
        return;
    }
}
