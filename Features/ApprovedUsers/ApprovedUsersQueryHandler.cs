using CashFlowAPI.Features.Common.Interfaces;

namespace CashFlowAPI.Features.ApprovedUsers;

public class ApprovedUsersQueryHandler :
    IQueryHandler<GetApprovedUsersQuery, ApprovedUsersReadModel>,
    IQueryHandler<GetApprovedUserQuery, ApprovedUserReadModel>
{
    private readonly IApprovedUsersRepository _approvedUsersRepository;

    public ApprovedUsersQueryHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }

    public async Task<ApprovedUsersReadModel> Handle(GetApprovedUsersQuery query, CancellationToken cancellationToken = default)
    {
        var approvedUsers = await _approvedUsersRepository.GetApprovedUsers(cancellationToken);
        return approvedUsers;
    }

    public async Task<ApprovedUserReadModel> Handle(GetApprovedUserQuery query, CancellationToken cancellationToken = default)
    {
        var isGuid = Guid.TryParse(query.GuidOrUsername, out Guid guid);
        return isGuid ?
            await _approvedUsersRepository.GetApprovedUserById(guid, cancellationToken) :
            await _approvedUsersRepository.GetApprovedUserByUsername(query.GuidOrUsername, cancellationToken);
    }
}
