using CashFlowAPI.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowAPI.Features.ApprovedUsers;

public record ApproveUserCommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; } = String.Empty;
}

public static class ApproveUserEndpoint
{
    public static async Task<IResult> Handle([FromBody]ApproveUserRequest request, ApprovedUsersCommandHandler handler, CancellationToken cancellationToken = default)
    {
        var command = new ApproveUserCommand
        {
            Username = request.Username
        };
        await handler.Handle(command, cancellationToken);
        return Results.Ok();
    }
}