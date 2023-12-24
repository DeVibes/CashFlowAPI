using CashFlowAPI.Features.ApprovedUsers;

namespace CashFlowAPI.Features.Common.Interfaces;

public interface IApprovedUsersRepository
{
    public Task<bool> ApproveUser(string username, Guid id, CancellationToken cancellationToken = default);
    public Task<bool> DisapproveUserById(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> DisapproveUserByUsername(string username, CancellationToken cancellationToken = default);
    public Task<ApprovedUsersReadModel> GetApprovedUsers(CancellationToken cancellationToken = default);
    public Task<ApprovedUserReadModel> GetApprovedUserById(Guid id, CancellationToken cancellationToken = default);
    public Task<ApprovedUserReadModel> GetApprovedUserByUsername(string username, CancellationToken cancellationToken = default);
}