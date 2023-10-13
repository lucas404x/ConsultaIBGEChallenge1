using ConsultaIbge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaIbge.Data.Mappings;

public class IbgeMapping : IEntityTypeConfiguration<Ibge>
{
    public void Configure(EntityTypeBuilder<Ibge> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnType("char(7)")
            ;

        builder.Property(i => i.State)
            .IsRequired()
            .HasColumnType("char(2)");

        builder.Property(i => i.City)
            .IsRequired();

        builder.ToTable("ibge");
    }
}
