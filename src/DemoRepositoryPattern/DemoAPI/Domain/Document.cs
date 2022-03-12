using MongoDB.Bson;

namespace DemoAPI.Domain
{
    internal abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
