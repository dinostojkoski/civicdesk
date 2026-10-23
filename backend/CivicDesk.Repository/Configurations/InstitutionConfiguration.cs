using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class InstitutionConfiguration :
    IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("Institutions");

        builder.HasKey(institution => institution.Id);

        builder.Property(institution => institution.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(institution => institution.Description)
            .HasMaxLength(2000);

        builder.Property(institution => institution.Type)
            .IsRequired();

        builder.Property(institution => institution.Status)
            .IsRequired();

        builder.Property(institution => institution.ContactEmail)
            .IsRequired()
            .HasMaxLength(254);

        builder.Property(institution => institution.ContactPhone)
            .HasMaxLength(30);

        builder.Property(institution => institution.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(institution => institution.LogoStorageKey)
            .HasMaxLength(500);

        builder.Property(institution => institution.BrandColor)
            .IsRequired()
            .HasMaxLength(7);

        builder.HasOne(institution => institution.Municipality)
            .WithMany()
            .HasForeignKey(institution => institution.MunicipalityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}