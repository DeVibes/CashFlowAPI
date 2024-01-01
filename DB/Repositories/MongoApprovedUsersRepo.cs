using CashFlowAPI.DB.Entities;
using CashFlowAPI.Features.ApprovedUsers;
using CashFlowAPI.Common.Interfaces;
using MongoDB.Driver;

namespace CashFlowAPI.Repositories;

public class MongoApprovedUsersRepo : IApprovedUsersRepository
{
    private readonly IMongoCollection<ApprovedUserDocument> _collection;
    private readonly FilterDefinitionBuilder<ApprovedUserDocument> filterBuilder = 
        Builders<ApprovedUserDocument>.Filter;
    public MongoApprovedUsersRepo(IMongoDatabase mongoDatabase)
    {
        SetUniqueUsernameField(mongoDatabase);
        _collection = mongoDatabase.GetCollection<ApprovedUserDocument>("approved-users");
    }

    public async Task<ApprovedUsersStatus> ApproveUser(string username, Guid guid, CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(new ApprovedUserDocument(guid, username));
            return ApprovedUsersStatus.Ok;
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            return ApprovedUsersStatus.AlreadyExist;
        }
    }

    public async Task<ApprovedUsersStatus> DisapproveUserById(Guid guid, CancellationToken cancellationToken = default)
    {
        var idFilter = filterBuilder.Eq(x => x.Id, guid);
        var dbResult = await _collection.DeleteOneAsync(idFilter);
        return dbResult.DeletedCount > 1 ? ApprovedUsersStatus.Ok : ApprovedUsersStatus.IDNotFound;
    }

    public async Task<ApprovedUsersStatus> DisapproveUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var usernameFilter = filterBuilder.Eq(x => x.Username, username);
        var dbResult = await _collection.DeleteOneAsync(usernameFilter);
        return dbResult.DeletedCount > 1 ? ApprovedUsersStatus.Ok : ApprovedUsersStatus.UsernameNotFound;
    }

    public async Task<ApprovedUserReadModel?> GetApprovedUserById(Guid guid, CancellationToken cancellationToken = default)
    {
        var idFilter = filterBuilder.Eq(x => x.Id, guid);
        var dbResult = await _collection.Find(idFilter).SingleOrDefaultAsync(cancellationToken);
        return dbResult?.ToReadModel();
    }

    public async Task<ApprovedUserReadModel?> GetApprovedUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var usernameFilter = filterBuilder.Eq(x => x.Username, username);
        var dbResult = await _collection.Find(usernameFilter).SingleOrDefaultAsync(cancellationToken);
        return dbResult?.ToReadModel();
    }

    public async Task<ApprovedUsersReadModel?> GetApprovedUsers(CancellationToken cancellationToken = default)
    {
        var dbResult = await _collection
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var approvedUsers = dbResult?.Select(doc => doc.ToReadModel());
        return new ApprovedUsersReadModel(approvedUsers);
    }

    private void SetUniqueUsernameField(IMongoDatabase database)
    {
        database.GetCollection<ApprovedUserDocument>("approved-users").Indexes.CreateOne(
            new CreateIndexModel<ApprovedUserDocument>(Builders<ApprovedUserDocument>.IndexKeys.Descending(doc => doc.Username),
            new CreateIndexOptions { Unique = true }));
    }
}
