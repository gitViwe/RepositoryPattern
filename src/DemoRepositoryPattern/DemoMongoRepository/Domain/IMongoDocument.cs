using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DemoMongoRepository.Domain;

/// <summary>
/// Represents the base implementation of MongoDb documents
/// </summary>
public interface IMongoDocument
{
    /// <summary>
    /// MongoDB document ID
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }

    /// <summary>
    /// Value is set during the creation of this element
    /// </summary>
    DateTime CreatedAt { get; }
}
