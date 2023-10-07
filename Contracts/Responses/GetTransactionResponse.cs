using CashFlowAPI.Features.Account;

namespace CashFlowAPI.Contracts.Responses;

public record GetTransactionResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public GetTransactionResponse(TransactionReadModel readModel)
    {
        Id = readModel.Id;
        Description = readModel.Description;
        Price = readModel.Price;
    }
}