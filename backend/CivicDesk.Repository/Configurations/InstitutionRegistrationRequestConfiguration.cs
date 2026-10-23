using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class InstitutionRegistrationRequestConfiguration :
    IEntityTypeConfiguration<InstitutionRegistrationRequest>
{
    public void Configure(EntityTypeBuilder<InstitutionRegistrationRequest> builder)
    {
        builder.ToTable("InstitutionRegistrationRequests");

        builder.HasKey(request => request.Id);

        builder.Property(request => request.InstitutionName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(request => request.InstitutionType)
            .IsRequired();

        builder.Property(request => request.ContactEmail)
            .IsRequired()
            .HasMaxLength(254);

        builder.Property(request => request.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(request => request.Status)
            .IsRequired();

        builder.Property(request => request.RejectionReason)
            .HasMaxLength(2000);

        builder.HasOne(request => request.ApplicantUser)
            .WithMany()
            .HasForeignKey(request => request.ApplicantUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(request => request.ReviewedByUser)
            .WithMany()
            .HasForeignKey(request => request.ReviewedByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(request => request.Municipality)
            .WithMany()
            .HasForeignKey(request => request.MunicipalityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(request => request.ApprovedInstitution)
            .WithOne()
            .HasForeignKey<InstitutionRegistrationRequest>(
                request => request.ApprovedInstitutionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}