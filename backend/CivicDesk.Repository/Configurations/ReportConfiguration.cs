using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class ReportConfiguration :
    IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasKey(report => report.Id);

        builder.Property(report => report.Reference)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(report => report.Reference)
            .IsUnique();

        builder.Property(report => report.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(report => report.ContactEmail)
            .HasMaxLength(254);

        builder.Property(report => report.ContactPhone)
            .HasMaxLength(30);

        builder.Property(report => report.GuestTrackingTokenHash)
            .HasMaxLength(256);

        builder.Property(report => report.ResolutionNote)
            .HasMaxLength(2000);

        builder.Property(report => report.RejectionReason)
            .HasMaxLength(2000);

        builder.HasOne(report => report.Institution)
            .WithMany()
            .HasForeignKey(report => report.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(report => report.ReportCategory)
            .WithMany()
            .HasForeignKey(report => report.ReportCategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(report => report.ReporterUser)
            .WithMany()
            .HasForeignKey(report => report.ReporterUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(report => report.AssignedMembership)
            .WithMany()
            .HasForeignKey(report => report.AssignedMembershipId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(report => report.Location, location =>
        {
            location.Property(value => value.Address)
                .HasColumnName("LocationAddress")
                .IsRequired()
                .HasMaxLength(500);

            location.Property(value => value.MunicipalityId)
                .HasColumnName("LocationMunicipalityId");

            location.Property(value => value.Latitude)
                .HasColumnName("LocationLatitude")
                .HasPrecision(9, 6);

            location.Property(value => value.Longitude)
                .HasColumnName("LocationLongitude")
                .HasPrecision(9, 6);

            location.HasOne(value => value.Municipality)
                .WithMany()
                .HasForeignKey(value => value.MunicipalityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Navigation(report => report.Location)
            .IsRequired();
    }
}