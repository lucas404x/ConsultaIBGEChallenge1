﻿using ConsultaIbge.Data.Uow;
using ConsultaIbge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultaIbge.Data.Context;

public class ApplicationContext : DbContext, IUnitOfWork
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Ibge> Ibges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(80)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        modelBuilder.Entity<Ibge>()
            .HasIndex(i => i.City)
            .HasDatabaseName("IX_IBGE_City");        

        modelBuilder.Entity<Ibge>()
            .HasIndex(i => i.State)
            .HasDatabaseName("IX_IBGE_State");

        modelBuilder.Entity<Ibge>()
            .HasIndex(i => i.Id)
            .HasDatabaseName("IX_IBGE_Id");
    }

    public async Task<bool> CommitAsync()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("DataCadastro").IsModified = false;
        }

        return await base.SaveChangesAsync() > 0;
    }
}