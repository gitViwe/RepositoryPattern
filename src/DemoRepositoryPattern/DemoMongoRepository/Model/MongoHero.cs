using DemoMongoRepository.Configuration;
using DemoMongoRepository.Domain;

namespace DemoMongoRepository.Model;

[BsonCollection("HeroCollection")]
public class MongoHero : MongoDocument
{
    public string Avatar { get; set; }
    public string Alias { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }
}
