using CashFlowAPI.Common;
using CashFlowAPI.Common.Interfaces;

namespace CashFlowAPI.Features.ApprovedUsers;

public record GetApprovedUsersQuery();
public record ApprovedUsersReadModel(IEnumerable<ApprovedUserReadModel>? Users);

public static class GetApprovedUsersEndpoint
{
    public static async Task<IResult> Handle(GetApprovedUsersQueryHandler handler, CancellationToken cancellationToken = default)
    {
        GetApprovedUsersQuery query = new();
        var response = await handler.Handle(query, cancellationToken);
        return Results.Ok(response);
    }
}

public class GetApprovedUsersQueryHandler : IQueryHandler<GetApprovedUsersQuery, ApprovedUsersReadModel>
{
    private IApprovedUsersRepository _approvedUsersRepository;

    public GetApprovedUsersQueryHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }

    public async Task<QueryResult<ApprovedUsersReadModel>> Handle(GetApprovedUsersQuery query, CancellationToken cancellationToken = default)
    {
        var approvedUsers = await _approvedUsersRepository.GetApprovedUsers(cancellationToken);
        var result = new QueryResult<ApprovedUsersReadModel>
        {
            Payload = approvedUsers
        };
        return result;
    }
}
