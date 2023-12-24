using CashFlowAPI.Features.Common.Interfaces;

namespace CashFlowAPI.Features.ApprovedUsers;

public class ApprovedUsersCommandHandler :
    ICommandHandler<ApproveUserCommand>,
    ICommandHandler<DisapproveUserCommand>
{
    private readonly IApprovedUsersRepository _approvedUsersRepository;

    public ApprovedUsersCommandHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }

    public async Task Handle(ApproveUserCommand command, CancellationToken cancellationToken = default)
    {
        await _approvedUsersRepository.ApproveUser(command.Username, command.Id, cancellationToken);
    }

    public async Task Handle(DisapproveUserCommand command, CancellationToken cancellationToken = default)
    {
        var isGuid = Guid.TryParse(command.GuidOrUsername, out Guid guid);
        if (isGuid)
            await _approvedUsersRepository.DisapproveUserById(guid, cancellationToken);
        else
            await _approvedUsersRepository.DisapproveUserByUsername(command.GuidOrUsername, cancellationToken);
    }

}
