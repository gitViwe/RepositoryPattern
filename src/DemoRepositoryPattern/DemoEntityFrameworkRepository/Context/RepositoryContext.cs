using DemoEntityFrameworkRepository.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoEntityFrameworkRepository.Context;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Villain> Villains { get; set; }
}
