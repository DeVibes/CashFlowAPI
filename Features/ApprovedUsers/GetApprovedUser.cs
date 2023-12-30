using CashFlowAPI.Common;
using CashFlowAPI.Common.Interfaces;

namespace CashFlowAPI.Features.ApprovedUsers;
public record GetApprovedUserQuery(string GuidOrUsername);
public record ApprovedUserReadModel(string Id, string Username);
public static class GetApprovedUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, GetApprovedUserQueryHandler handler, CancellationToken cancellationToken = default)
    {
        GetApprovedUserQuery query = new(guidOrUsername);
        var result = await handler.Handle(query, cancellationToken);
        return result.IsSuccess ? 
            Results.Ok(result) :
            Results.NotFound(result);
    }
}

public class GetApprovedUserQueryHandler : IQueryHandler<GetApprovedUserQuery, ApprovedUserReadModel>
{
    private string UsernameNotFoundStatus(string username) => 
        $"Username - {username} was not found";
    private string IDNotFoundStatus(string guid) => 
        $"ID - {guid} was not found";
    private readonly IApprovedUsersRepository _approvedUsersRepository;
    public GetApprovedUserQueryHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }
    public async Task<QueryResult<ApprovedUserReadModel>> Handle(GetApprovedUserQuery query, CancellationToken cancellationToken = default)
    {
        var isGuid = Guid.TryParse(query.GuidOrUsername, out Guid guid);
        var approvedUser = isGuid ?
            await _approvedUsersRepository.GetApprovedUserById(query.GuidOrUsername, cancellationToken) :
            await _approvedUsersRepository.GetApprovedUserByUsername(query.GuidOrUsername, cancellationToken);

        QueryResult<ApprovedUserReadModel> result = new()
        {
            IsSuccess = approvedUser is not null,
            Payload = approvedUser
        };
        if (approvedUser is not null)
            return result;
        if (isGuid)
            result.Status = IDNotFoundStatus(query.GuidOrUsername);
        else 
            result.Status = UsernameNotFoundStatus(query.GuidOrUsername);
        return result;
    }
}
