using CivicDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CivicDesk.Repository.Configurations;

public class NotificationConfiguration :
    IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(notification => notification.Id);

        builder.Property(notification => notification.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(notification => notification.RecipientUser)
            .WithMany()
            .HasForeignKey(notification => notification.RecipientUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(notification => notification.Report)
            .WithMany()
            .HasForeignKey(notification => notification.ReportId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(notification => new
        {
            notification.RecipientUserId,
            notification.CreatedAt
        });
    }
}