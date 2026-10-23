using CivicDesk.Domain.Common;

namespace CivicDesk.Domain.Models;

public class City : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public ICollection<Municipality> Municipalities { get; private set; } = new List<Municipality>();
    
    private City() {}

    public City(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "City name is required.", nameof(name));

        Name = name.Trim();
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "City name is required.", nameof(name));
        }

        Name = name.Trim();
        MarkUpdated();
    }
}