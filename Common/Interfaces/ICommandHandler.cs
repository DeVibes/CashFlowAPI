using CashFlowAPI.Common.HandlerResults;

namespace CashFlowAPI.Common.Interfaces;

public interface ICommandHandler<TCommand>
{
    public Task<CommandResult> Handle(TCommand command, CancellationToken cancellationToken = default);
}