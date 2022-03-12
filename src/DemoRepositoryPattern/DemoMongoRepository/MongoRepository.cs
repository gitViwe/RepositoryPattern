using DemoMongoRepository.Configuration;
using DemoMongoRepository.Domain;
using DemoMongoRepository.Settings;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DemoMongoRepository;

internal class MongoRepository<TMongoDocument> : IMongoRepository<TMongoDocument> where TMongoDocument : MongoDocument
{
    private readonly IMongoCollection<TMongoDocument> _collection;
    public MongoRepository(MongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<TMongoDocument>(GetCollectionName(typeof(TMongoDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        var data = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault();

        if (data is null)
        {
            throw new ArgumentException($"'{nameof(documentType)}' does not have the class attribute/decorator '{nameof(BsonCollectionAttribute)}'");
        }

        return ((BsonCollectionAttribute)data).CollectionName;
    }

    public IQueryable<TMongoDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public void DeleteById(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public void DeleteOne(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TMongoDocument> FilterBy(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TMongoDocument, bool>> filterExpression, Expression<Func<TMongoDocument, TProjected>> projectionExpression)
    {
        throw new NotImplementedException();
    }

    public TMongoDocument FindById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<TMongoDocument> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public TMongoDocument FindOne(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public Task<TMongoDocument> FindOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        throw new NotImplementedException();
    }

    public void InsertMany(ICollection<TMongoDocument> documents)
    {
        throw new NotImplementedException();
    }

    public Task InsertManyAsync(ICollection<TMongoDocument> documents)
    {
        throw new NotImplementedException();
    }

    public void InsertOne(TMongoDocument document)
    {
        throw new NotImplementedException();
    }

    public Task InsertOneAsync(TMongoDocument document)
    {
        throw new NotImplementedException();
    }

    public void ReplaceOne(TMongoDocument document)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceOneAsync(TMongoDocument document)
    {
        throw new NotImplementedException();
    }
}
