using CivicDesk.Domain.Common;

namespace CivicDesk.Domain.Models;

public class ReportAttachment : BaseEntity
{
    public Guid ReportId { get; private set; }
    public Report Report { get; private set; } = null!;

    public string StorageKey { get; private set; } = string.Empty;
    public string OriginalFileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }

    public DateTimeOffset UploadedAt { get; private set; }
        = DateTimeOffset.UtcNow;

    private ReportAttachment() { }

    public ReportAttachment(
        Guid reportId,
        string storageKey,
        string originalFileName,
        string contentType,
        long sizeBytes)
    {
        if (reportId == Guid.Empty)
            throw new ArgumentException(
                "Report is required.", nameof(reportId));

        if (string.IsNullOrWhiteSpace(storageKey))
            throw new ArgumentException(
                "Storage key is required.", nameof(storageKey));

        if (string.IsNullOrWhiteSpace(originalFileName))
            throw new ArgumentException(
                "Original file name is required.",
                nameof(originalFileName));

        if (string.IsNullOrWhiteSpace(contentType))
            throw new ArgumentException(
                "Content type is required.", nameof(contentType));

        if (sizeBytes <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(sizeBytes),
                "File size must be greater than zero.");

        ReportId = reportId;
        StorageKey = storageKey.Trim();
        OriginalFileName = originalFileName.Trim();
        ContentType = contentType.Trim();
        SizeBytes = sizeBytes;
    }
}