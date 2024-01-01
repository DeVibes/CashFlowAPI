using CashFlowAPI.Features.ApprovedUsers;

namespace CashFlowAPI.Common.Interfaces;

public interface IApprovedUsersRepository
{
    public Task<ApprovedUsersStatus> ApproveUser(string username, Guid guid, CancellationToken cancellationToken = default);
    public Task<ApprovedUsersStatus> DisapproveUserById(Guid guid, CancellationToken cancellationToken = default);
    public Task<ApprovedUsersStatus> DisapproveUserByUsername(string username, CancellationToken cancellationToken = default);
    public Task<ApprovedUsersReadModel?> GetApprovedUsers(CancellationToken cancellationToken = default);
    public Task<ApprovedUserReadModel?> GetApprovedUserById(Guid guid, CancellationToken cancellationToken = default);
    public Task<ApprovedUserReadModel?> GetApprovedUserByUsername(string username, CancellationToken cancellationToken = default);
}