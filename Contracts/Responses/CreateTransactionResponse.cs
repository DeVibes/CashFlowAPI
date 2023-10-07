namespace CashFlowAPI.Contracts.Responses;

public record CreateTransactionResponse
{
    public double Amount { get; set; }
    public string Description { get; set; }
}