
namespace DemoEntityFrameworkRepository.Domain;

/// <summary>
/// Represents the base implementation of Entity Framework entities
/// </summary>
public interface IBaseEntity
{
    /// <summary>
    /// The ID of the entity
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Value is set during the creation of this element
    /// </summary>
    DateTime CreatedAt { get; }
}
