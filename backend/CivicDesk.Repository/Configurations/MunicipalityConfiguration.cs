using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.ToTable("Municipalities");
        
        builder.HasKey(municipality => municipality.Id);
        
        builder.Property(municipality => municipality.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}