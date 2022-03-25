using System.ComponentModel.DataAnnotations;

namespace DemoMongoRepository.Model;

public class MongoVillainRequest
{
    public string Id { get; set; }
    [Required]
    public string StreetName { get; set; }
    [Required]
    public string Affiliation { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }
}
