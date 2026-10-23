using CivicDesk.Domain.Common;

namespace CivicDesk.Domain.Models;

public class ReportCategory : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public Guid InstitutionId { get; private set; }
    public Institution Institution { get; private set; } = null!;

    public bool IsActive { get; private set; } = true;

    private ReportCategory() { }

    public ReportCategory(Guid institutionId, string name)
    {
        if (institutionId == Guid.Empty)
            throw new ArgumentException(
                "Institution is required.", nameof(institutionId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Category name is required.", nameof(name));

        InstitutionId = institutionId;
        Name = name.Trim();
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Category name is required.", nameof(name));

        Name = name.Trim();
        MarkUpdated();
    }

    public void Disable()
    {
        if (!IsActive)
            return;

        IsActive = false;
        MarkUpdated();
    }

    public void Enable()
    {
        if (IsActive)
            return;

        IsActive = true;
        MarkUpdated();
    }
} 