using Microsoft.EntityFrameworkCore;
using NBomberFirst.Entities;

namespace NBomberFirst.Persistance;

public class MoviesDbContext : DbContext 
{
    public DbSet<Movie> Movies { get; set; }

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}