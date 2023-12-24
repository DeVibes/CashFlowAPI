using CashFlowAPI.Features.ApprovedUsers;

namespace CashFlowAPI.DB.Entities;

public class ApprovedUserDocument
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public ApprovedUserDocument() {}
    public ApprovedUserDocument(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    public ApprovedUserReadModel ToReadModel() => new(Id, Username);
}