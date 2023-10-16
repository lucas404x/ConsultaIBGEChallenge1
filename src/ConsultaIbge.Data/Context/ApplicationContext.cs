using ConsultaIbge.Core.Data;
using ConsultaIbge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Data.Context;

public class ApplicationContext : DbContext, IUnitOfWork
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Locality> Localities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(80)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        modelBuilder.Entity<Locality>()
            .HasIndex(i => i.City)
            .HasDatabaseName("IX_IBGE_City");        

        modelBuilder.Entity<Locality>()
            .HasIndex(i => i.State)
            .HasDatabaseName("IX_IBGE_State");

        modelBuilder.Entity<Locality>()
            .HasIndex(i => i.Id)
            .HasDatabaseName("IX_IBGE_Id");
    }

    public async Task<bool> CommitAsync()
    {
        return await base.SaveChangesAsync() > 0;
    }
}
