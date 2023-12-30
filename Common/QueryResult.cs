namespace CashFlowAPI.Common;

public record QueryResult<T> : HandlerResult
{
    public T? Payload { get; init; }
}