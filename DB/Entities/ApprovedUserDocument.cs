using CashFlowAPI.Features.ApprovedUsers;

namespace CashFlowAPI.DB.Entities;

public class ApprovedUserDocument
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public ApprovedUserDocument() {}
    public ApprovedUserDocument(Guid guid, string username)
    {
        Id = guid;
        Username = username;
    }

    public ApprovedUserReadModel ToReadModel() => new(Id, Username);
}