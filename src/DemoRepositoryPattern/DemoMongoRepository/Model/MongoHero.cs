using DemoMongoRepository.Configuration;
using DemoMongoRepository.Domain;

namespace DemoMongoRepository.Model;

[BsonCollection("Heroes")]
internal class MongoHero : MongoDocument
{
    public string Avatar { get; set; }
    public string Alias { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }
}

internal enum Elemental
{
    Nature,
    Fire,
    Wind,
    Poison
}

internal enum Morality
{
    SuperHero,
    Hero,
    Neutral,
    Vilian,
    SuperVilian
}
