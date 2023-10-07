namespace CashFlowAPI.Contracts.Requests;

public record CreateTransactionRequest
{
    public double Price { get; set; }
    public string Description { get; set; }
}