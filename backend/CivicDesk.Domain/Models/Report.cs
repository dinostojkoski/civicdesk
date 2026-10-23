using CivicDesk.Domain.Common;
using CivicDesk.Domain.Enums;

namespace CivicDesk.Domain.Models;

public class Report : BaseAuditableEntity
{
    public string Reference { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public Guid InstitutionId { get; private set; }
    public Institution Institution { get; private set; } = null!;

    public Guid ReportCategoryId { get; private set; }
    public ReportCategory ReportCategory { get; private set; } = null!;

    public Guid? ReporterUserId { get; private set; }
    public ApplicationUser? ReporterUser { get; private set; }

    public ReportLocation Location { get; private set; } = null!;

    public ReportStatus Status { get; private set; }
        = ReportStatus.Submitted;

    public ReportModerationStatus ModerationStatus { get; private set; }
        = ReportModerationStatus.Pending;

    public bool IsPrivate { get; private set; } = true;

    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }

    public string? GuestTrackingTokenHash { get; private set; }
    
    public Guid? AssignedMembershipId { get; private set; }

    public InstitutionMembership? AssignedMembership { get; private set; }

    public string? ResolutionNote { get; private set; }
    public string? RejectionReason { get; private set; }

    public DateTimeOffset? ClosedAt { get; private set; }
    
    public ICollection<ReportAttachment> Attachments { get; private set; }
        = new List<ReportAttachment>();

    private Report() { }

    public Report(
        string reference,
        string description,
        Guid institutionId,
        Guid reportCategoryId,
        ReportLocation location,
        Guid? reporterUserId = null,
        string? guestTrackingTokenHash = null,
        bool isPrivate = true,
        string? contactEmail = null,
        string? contactPhone = null)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new ArgumentException(
                "Report reference is required.", nameof(reference));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(
                "Description is required.", nameof(description));

        if (institutionId == Guid.Empty)
            throw new ArgumentException(
                "Institution is required.", nameof(institutionId));

        if (reportCategoryId == Guid.Empty)
            throw new ArgumentException(
                "Category is required.", nameof(reportCategoryId));

        ArgumentNullException.ThrowIfNull(location);

        if (reporterUserId == Guid.Empty)
            throw new ArgumentException(
                "Reporter identifier cannot be empty.",
                nameof(reporterUserId));

        if (!reporterUserId.HasValue &&
            string.IsNullOrWhiteSpace(guestTrackingTokenHash))
        {
            throw new ArgumentException(
                "Guest reports require a tracking token hash.",
                nameof(guestTrackingTokenHash));
        }

        Reference = reference.Trim();
        Description = description.Trim();

        InstitutionId = institutionId;
        ReportCategoryId = reportCategoryId;
        Location = location;
        ReporterUserId = reporterUserId;

        GuestTrackingTokenHash =
            string.IsNullOrWhiteSpace(guestTrackingTokenHash)
                ? null
                : guestTrackingTokenHash.Trim();

        IsPrivate = isPrivate;

        ContactEmail = string.IsNullOrWhiteSpace(contactEmail)
            ? null
            : contactEmail.Trim();

        ContactPhone = string.IsNullOrWhiteSpace(contactPhone)
            ? null
            : contactPhone.Trim();
    }
    
    public void StartReview()
    {
        EnsureStatus(ReportStatus.Submitted);

        Status = ReportStatus.UnderReview;
        MarkUpdated();
    }

    public void AssignTo(InstitutionMembership membership)
    {
        ArgumentNullException.ThrowIfNull(membership);

        EnsureStatus(ReportStatus.UnderReview);

        if (membership.InstitutionId != InstitutionId)
        {
            throw new InvalidOperationException(
                "The employee must belong to the report's institution.");
        }

        if (!membership.IsActive)
        {
            throw new InvalidOperationException(
                "Reports cannot be assigned to an inactive membership.");
        }

        AssignedMembershipId = membership.Id;
        AssignedMembership = membership;

        Status = ReportStatus.Assigned;
        MarkUpdated();
    }

    public void StartProgress()
    {
        EnsureStatus(ReportStatus.Assigned);

        if (!AssignedMembershipId.HasValue)
        {
            throw new InvalidOperationException(
                "The report must be assigned before work can start.");
        }

        Status = ReportStatus.InProgress;
        MarkUpdated();
    }

    public void Resolve(string? resolutionNote = null)
    {
        EnsureStatus(ReportStatus.InProgress);

        ResolutionNote = string.IsNullOrWhiteSpace(resolutionNote)
            ? null
            : resolutionNote.Trim();

        Status = ReportStatus.Resolved;
        ClosedAt = DateTimeOffset.UtcNow;

        MarkUpdated();
    }

    public void Reject(string reason)
    {
        EnsureStatus(ReportStatus.UnderReview);

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException(
                "A rejection reason is required.", nameof(reason));
        }

        RejectionReason = reason.Trim();

        Status = ReportStatus.Rejected;
        ClosedAt = DateTimeOffset.UtcNow;

        MarkUpdated();
    }

    private void EnsureStatus(ReportStatus expectedStatus)
    {
        if (Status != expectedStatus)
        {
            throw new InvalidOperationException(
                $"This action requires status '{expectedStatus}', " +
                $"but the current status is '{Status}'.");
        }
    }
}