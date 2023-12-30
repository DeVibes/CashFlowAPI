namespace CashFlowAPI.Common;

public record HandlerResult
{
    public bool IsSuccess { get; set; } = true;
    public string Status { get; set; } = string.Empty;
}