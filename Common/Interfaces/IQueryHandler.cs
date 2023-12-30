namespace CashFlowAPI.Common.Interfaces;

public interface IQueryHandler<TQuery, TResult>
{
    public Task<QueryResult<TResult>> Handle(TQuery query, CancellationToken cancellationToken = default);
}