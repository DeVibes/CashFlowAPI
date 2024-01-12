using CashFlowAPI.Common.HandlerResults;
using CashFlowAPI.Common.Interfaces;
using CashFlowAPI.Contracts.Responses;

namespace CashFlowAPI.Features.ApprovedUsers;
public record GetApprovedUserQuery(string GuidOrUsername);
public record ApprovedUserReadModel(Guid Id, string Username);
public static class GetApprovedUserEndpoint
{
    public static async Task<IResult> Handle(string guidOrUsername, GetApprovedUserQueryHandler handler, CancellationToken cancellationToken = default)
    {
        GetApprovedUserQuery query = new(guidOrUsername);
        var result = await handler.Handle(query, cancellationToken);

        var isUserNotFound = result.ErrorType.Equals(ApprovedUsersStatus.NotFound.ToString());
        if (isUserNotFound)
            return Results.NotFound(new APIErrorResponse(result.ErrorType, result.Message));
        return Results.Ok(new APIDataResposne(result.Message, result.Payload));
    }
}

public class GetApprovedUserQueryHandler : IQueryHandler<GetApprovedUserQuery, ApprovedUserReadModel>
{
    private string NotFoundStatus(string guidOrUsername) => 
        $"{guidOrUsername} was not found";
    private readonly IApprovedUsersRepository _approvedUsersRepository;
    public GetApprovedUserQueryHandler(IApprovedUsersRepository approvedUsersRepository)
    {
        _approvedUsersRepository = approvedUsersRepository;
    }
    public async Task<QueryResult<ApprovedUserReadModel>> Handle(GetApprovedUserQuery query, CancellationToken cancellationToken = default)
    {
        var isGuid = Guid.TryParse(query.GuidOrUsername, out Guid guid);
        var approvedUser = isGuid ?
            await _approvedUsersRepository.GetApprovedUserById(guid, cancellationToken) :
            await _approvedUsersRepository.GetApprovedUserByUsername(query.GuidOrUsername, cancellationToken);

        QueryResult<ApprovedUserReadModel> result = new()
        {
            IsSuccess = approvedUser is not null,
            Payload = approvedUser,
            Message = approvedUser is null ? NotFoundStatus(query.GuidOrUsername) : string.Empty,
            ErrorType = approvedUser is null ? ApprovedUsersStatus.NotFound.ToString() : ApprovedUsersStatus.Ok.ToString(),
        };
        return result;
    }
}
