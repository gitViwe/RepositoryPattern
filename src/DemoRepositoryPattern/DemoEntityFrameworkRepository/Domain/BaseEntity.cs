namespace DemoEntityFrameworkRepository.Domain;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt => DateTime.Now;
}
