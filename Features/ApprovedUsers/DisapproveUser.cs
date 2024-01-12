using CashFlowAPI.Common.HandlerResults;
using CashFlowAPI.Common.Interfaces;
using CashFlowAPI.Contracts.Responses;

namespace CashFlowAPI.Features.ApprovedUsers;

public record DisapproveUserCommand(string GuidOrUsername);
public static class DisapproveUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, DisapproveUserCommandHandler handler, CancellationToken cancellationToken = default)
    {
        DisapproveUserCommand command = new(guidOrUsername);
        var result = await handler.Handle(command);

        var isUserNotFound = result.ErrorType.Equals(ApprovedUsersStatus.NotFound.ToString());
        if (isUserNotFound)
            return Results.NotFound(new APIErrorResponse(result.ErrorType, result.Message));
        return Results.Ok(new APIOkResposne(result.Message));
    }
}

public class DisapproveUserCommandHandler : ICommandHandler<DisapproveUserCommand>
{
    private string DisapprovedSuccessfullyStatus(string guidOrUsername) => $"{guidOrUsername} disapproved";
    private string NotFoundStatus(string guidOrUsername) => $"Did not find id - {guidOrUsername}";
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
            Message = disapproveStatus switch
            {
                ApprovedUsersStatus.Ok => DisapprovedSuccessfullyStatus(command.GuidOrUsername),
                ApprovedUsersStatus.NotFound => NotFoundStatus(command.GuidOrUsername),
                _ => string.Empty
            },
            ErrorType = disapproveStatus switch
            {
                ApprovedUsersStatus.NotFound => ApprovedUsersStatus.NotFound.ToString(),
                _ => string.Empty
            }
        };
        return result;
    }
}
