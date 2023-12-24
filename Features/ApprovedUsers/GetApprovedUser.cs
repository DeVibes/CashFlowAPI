namespace CashFlowAPI.Features.ApprovedUsers;

public record GetApprovedUserQuery(string GuidOrUsername);
public record ApprovedUserReadModel(Guid Id, string Username);

public static class GetApprovedUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, ApprovedUsersQueryHandler handler, CancellationToken cancellationToken = default)
    {
        GetApprovedUserQuery query = new(guidOrUsername);
        var response = await handler.Handle(query, cancellationToken);
        return Results.Ok(response);
    }
}