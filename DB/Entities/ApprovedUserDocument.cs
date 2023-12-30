using CashFlowAPI.Features.ApprovedUsers;

namespace CashFlowAPI.DB.Entities;

public class ApprovedUserDocument
{
    public string Id { get; set; }
    public string Username { get; set; }
    public ApprovedUserDocument() {}
    public ApprovedUserDocument(string guid, string username)
    {
        Id = guid;
        Username = username;
    }

    public ApprovedUserReadModel ToReadModel() => new(Id, Username);
}