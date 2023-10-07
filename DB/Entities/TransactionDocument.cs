using CashFlowAPI.Features.Account;

namespace CashFlowAPI.DB.Entities;

public class TransactionDocument
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public TransactionDocument()
    {
        
    }

    public TransactionDocument(CreateTransactionCommand command)
    {
        Id = command.Id;
        Price = command.Price;
        Description = command.Description;
    }

    public TransactionReadModel ToReadModel() => new()
    {
        Id = Id,
        Description = Description,
        Price = Price
    };
}