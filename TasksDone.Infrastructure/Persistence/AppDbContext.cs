using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TasksDone.Domain.Entities;

namespace TasksDone.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<TeamProject> Projects => Set<TeamProject>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamProject>()
            .HasMany(p => p.Tasks)
            .WithOne() 
            .OnDelete(DeleteBehavior.Cascade);
    }
}
