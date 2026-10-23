using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class ReportCategoryConfiguration :
    IEntityTypeConfiguration<ReportCategory>
{
    public void Configure(EntityTypeBuilder<ReportCategory> builder)
    {
        builder.ToTable("ReportCategories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(category => category.IsActive)
            .IsRequired();

        builder.HasOne(category => category.Institution)
            .WithMany()
            .HasForeignKey(category => category.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}