using CashFlowAPI.Common;
using CashFlowAPI.Common.Interfaces;
using CashFlowAPI.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowAPI.Features.ApprovedUsers;

public record ApproveUserCommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; } = string.Empty;
}

public static class ApproveUserEndpoint
{
    public static async Task<IResult> Handle([FromBody]ApproveUserRequest request, ApproveUserCommandHandler handler, CancellationToken cancellationToken = default)
    {
        var command = new ApproveUserCommand
        {
            Username = request.Username
        };

        var result = await handler.Handle(command, cancellationToken); 
        return result.IsSuccess ? 
            Results.CreatedAtRoute
            (
                routeName: nameof(GetApprovedUserEndpoint),
                routeValues: new { guidOrUsername = request.Username },
                value: request.Username
            ) : Results.BadRequest(result.Status);
    }
}

public class ApproveUserCommandHandler : ICommandHandler<ApproveUserCommand>
{
    private string SuccessStatus(string username) => $"Username {username} approved.";
    private string AlreadyExistStatus(string username) => $"Username {username} already approved";
    private readonly IApprovedUsersRepository _approvedUsersRepository;

    public ApproveUserCommandHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }

    public async Task<CommandResult> Handle(ApproveUserCommand command, CancellationToken cancellationToken = default)
    {
        var approveStatus = await _approvedUsersRepository.ApproveUser(command.Username, command.Id, cancellationToken);
        CommandResult result = new()
        {
            IsSuccess = approveStatus is ApprovedUsersStatus.Ok,
            Status = approveStatus switch
            {
                ApprovedUsersStatus.Ok => SuccessStatus(command.Username),
                ApprovedUsersStatus.AlreadyExist => AlreadyExistStatus(command.Username),
                _ => string.Empty
            }
        };
        return result;
    }
}
