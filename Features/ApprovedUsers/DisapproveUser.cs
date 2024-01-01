using CashFlowAPI.Common;
using CashFlowAPI.Common.Interfaces;

namespace CashFlowAPI.Features.ApprovedUsers;

public record DisapproveUserCommand(string GuidOrUsername);
public static class DisapproveUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, DisapproveUserCommandHandler handler, CancellationToken cancellationToken = default)
    {
        DisapproveUserCommand command = new(guidOrUsername);
        var result = await handler.Handle(command);
        return result.IsSuccess ? 
            Results.Ok(result.Status) :
            Results.NotFound(result.Status);
    }
}

public class DisapproveUserCommandHandler : ICommandHandler<DisapproveUserCommand>
{
    private string DisapprovedSuccessfullyStatus(string username) => $"Username {username} disapproved ";
    private string IDNotFoundStatus(string guid) => $"Did not find id - {guid}";
    private string UsernameNotFoundStatus(string username) => $"Did not find username - {username}";
    private readonly IApprovedUsersRepository _approvedUsersRepository;

    public DisapproveUserCommandHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }

    public async Task<CommandResult> Handle(DisapproveUserCommand command, CancellationToken cancellationToken = default)
    {
        var isGuid = Guid.TryParse(command.GuidOrUsername, out Guid guid);
        var disapproveStatus = isGuid ?
            await _approvedUsersRepository.DisapproveUserById(guid, cancellationToken) :
            await _approvedUsersRepository.DisapproveUserByUsername(command.GuidOrUsername, cancellationToken);
        CommandResult result = new()
        {
            IsSuccess = disapproveStatus is ApprovedUsersStatus.Ok,
            Status = disapproveStatus switch
            {
                ApprovedUsersStatus.Ok => DisapprovedSuccessfullyStatus(command.GuidOrUsername),
                ApprovedUsersStatus.IDNotFound => IDNotFoundStatus(command.GuidOrUsername),
                ApprovedUsersStatus.UsernameNotFound => UsernameNotFoundStatus(command.GuidOrUsername),
                _ => string.Empty
            }
        };
        return result;
    }
}
