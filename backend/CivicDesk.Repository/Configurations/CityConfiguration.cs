using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");
        
        builder.HasKey(city => city.Id);
        
        builder.Property(city => city.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(city => city.Municipalities)
            .WithOne(m => m.City)
            .HasForeignKey(m => m.CityId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}