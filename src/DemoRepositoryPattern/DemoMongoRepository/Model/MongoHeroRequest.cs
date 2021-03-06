using System.ComponentModel.DataAnnotations;

namespace DemoMongoRepository.Model;

public class MongoHeroRequest
{
    public string Id { get; set; }
    [Required]
    public string Avatar { get; set; }
    [Required]
    public string Alias { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }
}
