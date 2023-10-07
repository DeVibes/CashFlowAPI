namespace CashFlowAPI.Features.Common.Interfaces;

public interface IQueryHandler<TQuery, TResult>
{
    public Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
}