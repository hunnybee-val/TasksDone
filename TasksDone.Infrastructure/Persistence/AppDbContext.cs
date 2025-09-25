using Microsoft.EntityFrameworkCore;
using TasksDone.Domain.Entities;

namespace TasksDone.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<TeamProject> Projects { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tasksdone.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamProject>()
            .HasMany(p => p.Tasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
