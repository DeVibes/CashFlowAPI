namespace CashFlowAPI.Features.ApprovedUsers;

public record DisapproveUserCommand(string GuidOrUsername);
public static class DisapproveUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, ApprovedUsersCommandHandler handler, CancellationToken cancellationToken = default)
    {
        DisapproveUserCommand command = new(guidOrUsername);
        await handler.Handle(command);
        return Results.Ok();
    }
}