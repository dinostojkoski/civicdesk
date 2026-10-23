using CivicDesk.Domain.Common;
using CivicDesk.Domain.Enums;

namespace CivicDesk.Domain.Models;

public class Institution : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public InstitutionType Type { get; private set; }
    public InstitutionStatus Status { get; private set; }

    public string ContactEmail { get; private set; } = string.Empty;
    public string? ContactPhone { get; private set; }

    public string Address { get; private set; } = string.Empty;

    public Guid MunicipalityId { get; private set; }
    public Municipality Municipality { get; private set; } = null!;

    public string? LogoStorageKey { get; private set; }
    public string BrandColor { get; private set; } = "#087D76";

    private Institution() { }

    public Institution(
        string name,
        InstitutionType type,
        string contactEmail,
        string address,
        Guid municipalityId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Institution name is required.", nameof(name));

        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

        if (string.IsNullOrWhiteSpace(contactEmail))
            throw new ArgumentException(
                "Contact email is required.", nameof(contactEmail));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Address is required.", nameof(address));

        if (municipalityId == Guid.Empty)
            throw new ArgumentException(
                "Municipality is required.", nameof(municipalityId));

        Name = name.Trim();
        Type = type;
        ContactEmail = contactEmail.Trim();
        Address = address.Trim();
        MunicipalityId = municipalityId;

        Status = InstitutionStatus.Active;
    }

    public void UpdateContactDetails(
        string contactEmail,
        string? contactPhone,
        string address)
    {
        if (string.IsNullOrWhiteSpace(contactEmail))
            throw new ArgumentException(
                "Contact email is required.", nameof(contactEmail));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Address is required.", nameof(address));

        ContactEmail = contactEmail.Trim();
        ContactPhone = string.IsNullOrWhiteSpace(contactPhone)
            ? null
            : contactPhone.Trim();
        Address = address.Trim();

        MarkUpdated();
    }

    public void Suspend()
    {
        if (Status == InstitutionStatus.Suspended)
            return;

        Status = InstitutionStatus.Suspended;
        MarkUpdated();
    }

    public void Activate()
    {
        if (Status == InstitutionStatus.Active)
            return;

        Status = InstitutionStatus.Active;
        MarkUpdated();
    }
}