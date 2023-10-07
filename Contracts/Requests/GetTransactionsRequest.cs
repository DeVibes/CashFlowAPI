namespace CashFlowAPI.Contracts.Requests;

public record GetTransactionsRequest
{
    public int PageNumber { get; set; }
}