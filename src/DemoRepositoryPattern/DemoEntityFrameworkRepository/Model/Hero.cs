using DemoEntityFrameworkRepository.Domain;

namespace DemoEntityFrameworkRepository.Model;

public class Hero : BaseEntity
{
    public Hero()
    {
        this.Elementals = new HashSet<Elemental>();
    }

    public string Avatar { get; set; }
    public string Alias { get; set; }
    public virtual ICollection<Elemental> Elementals { get; set; }
    public Morality Morality { get; set; }
}
