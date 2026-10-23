using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class ReportAttachmentConfiguration :
    IEntityTypeConfiguration<ReportAttachment>
{
    public void Configure(EntityTypeBuilder<ReportAttachment> builder)
    {
        builder.ToTable("ReportAttachments");

        builder.HasKey(attachment => attachment.Id);

        builder.Property(attachment => attachment.StorageKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(attachment => attachment.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(attachment => attachment.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(attachment => attachment.Report)
            .WithMany(report => report.Attachments)
            .HasForeignKey(attachment => attachment.ReportId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}