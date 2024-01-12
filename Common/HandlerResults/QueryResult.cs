namespace CashFlowAPI.Common.HandlerResults;

public record QueryResult<T> : HandlerResult
{
    public T? Payload { get; init; }
}