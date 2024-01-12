using CashFlowAPI.Common.HandlerResults;
using CashFlowAPI.Common.Interfaces;
using CashFlowAPI.Contracts.Requests;
using CashFlowAPI.Contracts.Responses;
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
        var isUserAlreadyExists = result.ErrorType.Equals(ApprovedUsersStatus.AlreadyExist.ToString());
        if (isUserAlreadyExists)
            return Results.BadRequest(new APIErrorResponse(result.ErrorType, result.Message));
        return Results.CreatedAtRoute(
            nameof(GetApprovedUserEndpoint),
            new { guidOrUsername = request.Username },
            result.Message
            );
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
            Message = approveStatus switch
            {
                ApprovedUsersStatus.Ok => SuccessStatus(command.Username),
                ApprovedUsersStatus.AlreadyExist => AlreadyExistStatus(command.Username),
                _ => string.Empty
            }
        };
        if (approveStatus is ApprovedUsersStatus.AlreadyExist)
            result.ErrorType = ApprovedUsersStatus.AlreadyExist.ToString();
        return result;
    }
}
