using Microsoft.AspNetCore.Identity;

namespace CivicDesk.Domain.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; private set; } = string.Empty;
    
    public Guid? MunicipalityId { get; private set; }
    public Municipality? Municipality { get; private set; }
    
    public DateTimeOffset CreatedAt { get; private set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; private set; }

    public ApplicationUser() { }

    public ApplicationUser(string name, Guid? municipalityId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Name is required.", nameof(name));

        if (municipalityId == Guid.Empty)
            throw new ArgumentException(
                "Municipality identifier cannot be empty.", nameof(municipalityId));

        Id = Guid.NewGuid();
        Name = name.Trim();
        MunicipalityId = municipalityId;
    }

    public void UpdateProfile(string name, Guid? municipalityId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Name is required.", nameof(name));

        if (municipalityId == Guid.Empty)
            throw new ArgumentException(
                "Municipality identifier cannot be empty.", nameof(municipalityId));

        Name = name.Trim();
        MunicipalityId = municipalityId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}