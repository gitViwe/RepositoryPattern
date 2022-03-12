using MongoDB.Bson;

namespace DemoMongoRepository.Domain;

public abstract class MongoDocument : IMongoDocument
{
    public ObjectId Id { get; set; }

    public DateTime CreatedAt => Id.CreationTime;
}
