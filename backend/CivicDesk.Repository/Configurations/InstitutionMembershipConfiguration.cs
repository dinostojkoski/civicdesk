using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class InstitutionMembershipConfiguration :
    IEntityTypeConfiguration<InstitutionMembership>
{
    public void Configure(EntityTypeBuilder<InstitutionMembership> builder)
    {
        builder.ToTable("InstitutionMemberships");

        builder.HasKey(membership => membership.Id);

        builder.Property(membership => membership.Role)
            .IsRequired();

        builder.Property(membership => membership.IsActive)
            .IsRequired();

        builder.HasOne(membership => membership.User)
            .WithMany()
            .HasForeignKey(membership => membership.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(membership => membership.Institution)
            .WithMany()
            .HasForeignKey(membership => membership.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(membership => new
            {
                membership.UserId,
                membership.InstitutionId
            })
            .IsUnique();
    }
}