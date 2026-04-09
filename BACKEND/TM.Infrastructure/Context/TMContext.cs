using Microsoft.EntityFrameworkCore;
using TM.Domain.Entity;

namespace TM.Infrastructure.Context.TMContext;

public class TMContext : DbContext
{
    public TMContext()
    {}

    public TMContext(DbContextOptions<TMContext> options):base(options)
    {}

    public DbSet<TaskItem> TaskItem {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var connection = "Server=database;Database=tm_db;User Id=sa;Password=${SA_PASSWORD};TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.CreatedDate).IsRequired();
            entity.Property(t => t.Description).IsRequired();
            entity.Property(t => t.Name).IsRequired();
            entity.Property(t => t.Status).IsRequired();
            entity.Property(t => t.UpdatedDate).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}