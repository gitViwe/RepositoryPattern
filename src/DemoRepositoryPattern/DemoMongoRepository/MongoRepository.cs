using DemoMongoRepository.Configuration;
using DemoMongoRepository.Domain;
using DemoMongoRepository.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DemoMongoRepository;

public class MongoRepository<TMongoDocument> : IMongoRepository<TMongoDocument> where TMongoDocument : MongoDocument
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

    public virtual IEnumerable<TMongoDocument> FilterBy(
        Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TMongoDocument, bool>> filterExpression,
        Expression<Func<TMongoDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TMongoDocument FindOne(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual Task<TMongoDocument> FindOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual TMongoDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, objectId);
        return _collection.Find(filter).SingleOrDefault();
    }

    public virtual Task<TMongoDocument> FindByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }


    public virtual void InsertOne(TMongoDocument document)
    {
        _collection.InsertOne(document);
    }

    public virtual Task InsertOneAsync(TMongoDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    public void InsertMany(ICollection<TMongoDocument> documents)
    {
        _collection.InsertMany(documents);
    }


    public virtual async Task InsertManyAsync(ICollection<TMongoDocument> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public void ReplaceOne(TMongoDocument document)
    {
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, document.Id);
        _collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task ReplaceOneAsync(TMongoDocument document)
    {
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await _collection.FindOneAndReplaceAsync(filter, document);
    }

    public void DeleteOne(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        _collection.FindOneAndDelete(filterExpression);
    }

    public Task DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }

    public void DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, objectId);
        _collection.FindOneAndDelete(filter);
    }

    public Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public void DeleteMany(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        _collection.DeleteMany(filterExpression);
    }

    public Task DeleteManyAsync(Expression<Func<TMongoDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }

    public IQueryable<TMongoDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }
}
