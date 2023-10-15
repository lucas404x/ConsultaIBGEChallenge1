using ConsultaIbge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultaIbge.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .HasColumnType("char(7)");

        builder.Property(i => i.Name)
            .IsRequired();

        builder.Property(i => i.Email)
            .IsRequired();

        builder.Property(i => i.Password)
            .IsRequired();

        builder.ToTable("users");
    }
}
