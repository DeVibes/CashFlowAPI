namespace CashFlowAPI.Features.ApprovedUsers;

public record GetApprovedUsersQuery();
public record ApprovedUsersReadModel(IEnumerable<ApprovedUserReadModel> Users);

public static class GetApprovedUsersEndpoint
{
    public static async Task<IResult> Handle(ApprovedUsersQueryHandler handler, CancellationToken cancellationToken = default)
    {
        GetApprovedUsersQuery query = new();
        var response = await handler.Handle(query, cancellationToken);
        return Results.Ok(response);
    }
}