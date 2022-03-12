using DemoMongoRepository.Domain;
using System.Linq.Expressions;

namespace DemoMongoRepository;

public interface IMongoRepository<TMongoDocument> where TMongoDocument : IMongoDocument
{
    IQueryable<TMongoDocument> AsQueryable();

    IEnumerable<TMongoDocument> FilterBy(
        Expression<Func<TMongoDocument, bool>> filterExpression);

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TMongoDocument, bool>> filterExpression,
        Expression<Func<TMongoDocument, TProjected>> projectionExpression);

    TMongoDocument FindOne(Expression<Func<TMongoDocument, bool>> filterExpression);

    Task<TMongoDocument> FindOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression);

    TMongoDocument FindById(string id);

    Task<TMongoDocument> FindByIdAsync(string id);

    void InsertOne(TMongoDocument document);

    Task InsertOneAsync(TMongoDocument document);

    void InsertMany(ICollection<TMongoDocument> documents);

    Task InsertManyAsync(ICollection<TMongoDocument> documents);

    void ReplaceOne(TMongoDocument document);

    Task ReplaceOneAsync(TMongoDocument document);

    void DeleteOne(Expression<Func<TMongoDocument, bool>> filterExpression);

    Task DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression);

    void DeleteById(string id);

    Task DeleteByIdAsync(string id);

    void DeleteMany(Expression<Func<TMongoDocument, bool>> filterExpression);

    Task DeleteManyAsync(Expression<Func<TMongoDocument, bool>> filterExpression);
}
