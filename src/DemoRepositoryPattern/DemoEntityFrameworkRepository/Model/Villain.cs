using DemoEntityFrameworkRepository.Domain;

namespace DemoEntityFrameworkRepository.Model;

public class Villain : BaseEntity
{
    public Villain()
    {
        this.Elementals = new HashSet<Elemental>();
    }

    public string StreetName { get; set; }
    public string Affiliation { get; set; }
    public virtual ICollection<Elemental> Elementals { get; set; }
    public Morality Morality { get; set; }
}
