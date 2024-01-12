namespace CashFlowAPI.Common.HandlerResults;

public record HandlerResult
{
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public string ErrorType { get; set; } = string.Empty;
}