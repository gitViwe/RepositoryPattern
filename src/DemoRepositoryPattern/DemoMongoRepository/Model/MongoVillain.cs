using DemoMongoRepository.Configuration;
using DemoMongoRepository.Domain;

namespace DemoMongoRepository.Model;

[BsonCollection("VillainCollection")]
public class MongoVillain : MongoDocument
{
    public string StreetName { get; set; }
    public string Affiliation { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }
}
