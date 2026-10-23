using CivicDesk.Domain.Common;
using CivicDesk.Domain.Enums;

namespace CivicDesk.Domain.Models;

public class InstitutionRegistrationRequest : BaseAuditableEntity
{
    public Guid ApplicantUserId { get; private set; }
    public ApplicationUser ApplicantUser { get; private set; } = null!;

    public string InstitutionName { get; private set; } = string.Empty;
    public InstitutionType InstitutionType { get; private set; }

    public string ContactEmail { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;

    public Guid MunicipalityId { get; private set; }
    public Municipality Municipality { get; private set; } = null!;

    public InstitutionRegistrationRequestStatus Status { get; private set; }
        = InstitutionRegistrationRequestStatus.Pending;

    public Guid? ReviewedByUserId { get; private set; }
    public ApplicationUser? ReviewedByUser { get; private set; }
    
    public DateTimeOffset? ReviewedAt { get; private set; }
    public string? RejectionReason { get; private set; }

    public Guid? ApprovedInstitutionId { get; private set; }
    public Institution? ApprovedInstitution { get; private set; }

    private InstitutionRegistrationRequest() { }

    public InstitutionRegistrationRequest(
        Guid applicantUserId,
        string institutionName,
        InstitutionType institutionType,
        string contactEmail,
        string address,
        Guid municipalityId)
    {
        if (applicantUserId == Guid.Empty)
            throw new ArgumentException(
                "Applicant is required.", nameof(applicantUserId));

        if (string.IsNullOrWhiteSpace(institutionName))
            throw new ArgumentException(
                "Institution name is required.", nameof(institutionName));

        if (!Enum.IsDefined(institutionType))
            throw new ArgumentOutOfRangeException(nameof(institutionType));

        if (string.IsNullOrWhiteSpace(contactEmail))
            throw new ArgumentException(
                "Contact email is required.", nameof(contactEmail));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Address is required.", nameof(address));

        if (municipalityId == Guid.Empty)
            throw new ArgumentException(
                "Municipality is required.", nameof(municipalityId));

        ApplicantUserId = applicantUserId;
        InstitutionName = institutionName.Trim();
        InstitutionType = institutionType;
        ContactEmail = contactEmail.Trim();
        Address = address.Trim();
        MunicipalityId = municipalityId;
    }

    public void Approve(Guid institutionId, Guid reviewerUserId)
    {
        EnsurePending();

        if (institutionId == Guid.Empty)
            throw new ArgumentException(
                "Institution is required.", nameof(institutionId));

        if (reviewerUserId == Guid.Empty)
            throw new ArgumentException(
                "Reviewer is required.", nameof(reviewerUserId));

        Status = InstitutionRegistrationRequestStatus.Approved;
        ApprovedInstitutionId = institutionId;
        ReviewedByUserId = reviewerUserId;
        ReviewedAt = DateTimeOffset.UtcNow;

        MarkUpdated();
    }

    public void Reject(string reason, Guid reviewerUserId)
    {
        EnsurePending();

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException(
                "Rejection reason is required.", nameof(reason));

        if (reviewerUserId == Guid.Empty)
            throw new ArgumentException(
                "Reviewer is required.", nameof(reviewerUserId));

        Status = InstitutionRegistrationRequestStatus.Rejected;
        RejectionReason = reason.Trim();
        ReviewedByUserId = reviewerUserId;
        ReviewedAt = DateTimeOffset.UtcNow;

        MarkUpdated();
    }

    private void EnsurePending()
    {
        if (Status != InstitutionRegistrationRequestStatus.Pending)
            throw new InvalidOperationException(
                "This registration request has already been reviewed.");
    }
}