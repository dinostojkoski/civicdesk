using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class ApplicationUserConfiguration : 
    IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(user => user.Municipality)
            .WithMany()
            .HasForeignKey(user => user.MunicipalityId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}