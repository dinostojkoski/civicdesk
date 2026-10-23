using CivicDesk.Domain.Common;

namespace CivicDesk.Domain.Models;

public class Municipality : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    
    public Guid? CityId { get; private set; }
    public City? City { get; private set; }
    
    private Municipality() {}
    
    public Municipality(string name, Guid? cityId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Municipality name is required.",  nameof(name));
        
        if (cityId == Guid.Empty)
            throw new ArgumentException(
                "City ID cannot be empty.", nameof(cityId));
        
        Name = name.Trim();
        CityId = cityId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Municipality name is required.", nameof(name));

        Name = name.Trim();
        MarkUpdated();
    }
}