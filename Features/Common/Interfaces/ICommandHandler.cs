namespace CashFlowAPI.Features.Common.Interfaces;

public interface ICommandHandler<TCommand>
{
    public Task Handle(TCommand command, CancellationToken cancellationToken = default);
}