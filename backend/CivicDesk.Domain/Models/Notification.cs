using CivicDesk.Domain.Common;

namespace CivicDesk.Domain.Models;

public class Notification : BaseEntity
{
    public Guid RecipientUserId { get; private set; }
    public ApplicationUser RecipientUser { get; private set; } = null!;

    public Guid ReportId { get; private set; }
    public Report Report { get; private set; } = null!;

    public string Message { get; private set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; private set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? ReadAt { get; private set; }

    private Notification() { }

    public Notification(
        Guid recipientUserId,
        Guid reportId,
        string message)
    {
        if (recipientUserId == Guid.Empty)
            throw new ArgumentException(
                "Recipient is required.", nameof(recipientUserId));

        if (reportId == Guid.Empty)
            throw new ArgumentException(
                "Report is required.", nameof(reportId));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException(
                "Notification message is required.", nameof(message));

        RecipientUserId = recipientUserId;
        ReportId = reportId;
        Message = message.Trim();
    }

    public void MarkAsRead()
    {
        if (ReadAt.HasValue)
            return;

        ReadAt = DateTimeOffset.UtcNow;
    }
}