using CashFlowAPI.DB.Entities;
using CashFlowAPI.Features.ApprovedUsers;
using CashFlowAPI.Features.Common.Interfaces;
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

    public async Task<bool> ApproveUser(string username, Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(new ApprovedUserDocument(id, username));
        return true;
    }

    public async Task<bool> DisapproveUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var idFilter = filterBuilder.Eq(x => x.Id, id);
        var dbResult = await _collection.DeleteOneAsync(idFilter);
        return dbResult.DeletedCount > 1;
    }

    public async Task<bool> DisapproveUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var usernameFilter = filterBuilder.Eq(x => x.Username, username);
        var dbResult = await _collection.DeleteOneAsync(usernameFilter );
        return dbResult.DeletedCount > 1;
    }

    public async Task<ApprovedUserReadModel> GetApprovedUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var idFilter = filterBuilder.Eq(x => x.Id, id);
        var dbResult = await _collection.Find(idFilter).SingleOrDefaultAsync(cancellationToken);
        return dbResult.ToReadModel();
    }

    public async Task<ApprovedUserReadModel> GetApprovedUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var usernameFilter = filterBuilder.Eq(x => x.Username, username);
        var dbResult = await _collection.Find(username).SingleOrDefaultAsync(cancellationToken);
        return dbResult.ToReadModel();
    }

    public async Task<ApprovedUsersReadModel> GetApprovedUsers(CancellationToken cancellationToken = default)
    {
        var dbResult = await _collection
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var approvedUsers = dbResult.Select(doc => doc.ToReadModel());
        return new ApprovedUsersReadModel(approvedUsers);
    }

    private void SetUniqueUsernameField(IMongoDatabase database)
    {
        database.GetCollection<ApprovedUserDocument>("approved-users").Indexes.CreateOne(
            new CreateIndexModel<ApprovedUserDocument>(Builders<ApprovedUserDocument>.IndexKeys.Descending(doc => doc.Username),
            new CreateIndexOptions { Unique = true }));
    }
}
