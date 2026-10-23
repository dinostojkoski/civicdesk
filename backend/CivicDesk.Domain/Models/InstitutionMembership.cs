using CivicDesk.Domain.Common;
using CivicDesk.Domain.Enums;

namespace CivicDesk.Domain.Models;

public class InstitutionMembership : BaseAuditableEntity
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;

    public Guid InstitutionId { get; private set; }
    public Institution Institution { get; private set; } = null!;

    public InstitutionMembershipRole Role { get; private set; }
    public bool IsActive { get; private set; } = true;

    private InstitutionMembership() { }

    public InstitutionMembership(
        Guid userId,
        Guid institutionId,
        InstitutionMembershipRole role)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(
                "User is required.", nameof(userId));

        if (institutionId == Guid.Empty)
            throw new ArgumentException(
                "Institution is required.", nameof(institutionId));

        if (!Enum.IsDefined(role))
            throw new ArgumentOutOfRangeException(nameof(role));

        UserId = userId;
        InstitutionId = institutionId;
        Role = role;
    }

    public void ChangeRole(InstitutionMembershipRole role)
    {
        if (!Enum.IsDefined(role))
            throw new ArgumentOutOfRangeException(nameof(role));

        if (Role == role)
            return;

        Role = role;
        MarkUpdated();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        MarkUpdated();
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        MarkUpdated();
    }
}