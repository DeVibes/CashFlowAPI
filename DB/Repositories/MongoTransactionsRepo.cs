using CashFlowAPI.DB.Entities;
using CashFlowAPI.Features.Account;
using CashFlowAPI.Features.Common.Interfaces;
using MongoDB.Driver;

namespace CashFlowAPI.Repositories;

public class MongoTransactionsRepo : ITransactionsRepository
{
    private readonly IMongoCollection<TransactionDocument> _collection;
    private readonly FilterDefinitionBuilder<TransactionDocument> filterBuilder = 
        Builders<TransactionDocument>.Filter;
    public MongoTransactionsRepo(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<TransactionDocument>("transactions");
    }

    public Task<bool> CreateTransaction(CreateTransactionCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteTransaction(DeleteTransactionCommand command, CancellationToken cancellationToken = default)
    {
        var filter = filterBuilder.Eq(x => x.Id, command.Id);
        var dbResult = await _collection
            .DeleteOneAsync(filter);
        return dbResult.DeletedCount == 1; 
    }

    public async Task<TransactionReadModel> GetTransaction(GetTransactionQuery query, CancellationToken cancellationToken = default)
    {
        var filter = filterBuilder.Eq(x => x.Id, query.Id);
        var dbResult = await _collection
            .Find(filter)
            .SingleOrDefaultAsync(cancellationToken);
        return dbResult.ToReadModel();
    }

    public async Task<TransactionsReadModel> GetTransactions(GetTransactionsQuery query, CancellationToken cancellationToken = default)
    {
        var dbResult = await _collection
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var transactions = dbResult.Select(doc => doc.ToReadModel());

        return new TransactionsReadModel(transactions);
    }
}
