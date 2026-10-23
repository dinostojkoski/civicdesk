namespace CivicDesk.Domain.Models;

public class ReportLocation
{
    public string Address { get; private set; } = string.Empty;

    public Guid MunicipalityId { get; private set; }
    public Municipality Municipality { get; private set; } = null!;

    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    private ReportLocation() { }

    public ReportLocation(
        string address,
        Guid municipalityId,
        decimal? latitude = null,
        decimal? longitude = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Address or landmark is required.", nameof(address));

        if (municipalityId == Guid.Empty)
            throw new ArgumentException(
                "Municipality is required.", nameof(municipalityId));

        if (latitude.HasValue != longitude.HasValue)
            throw new ArgumentException(
                "Latitude and longitude must be provided together.");

        if (latitude.HasValue &&
            (latitude.Value < -90m || latitude.Value > 90m))
        {
            throw new ArgumentOutOfRangeException(
                nameof(latitude),
                "Latitude must be between -90 and 90.");
        }

        if (longitude.HasValue &&
            (longitude.Value < -180m || longitude.Value > 180m))
        {
            throw new ArgumentOutOfRangeException(
                nameof(longitude),
                "Longitude must be between -180 and 180.");
        }

        Address = address.Trim();
        MunicipalityId = municipalityId;
        Latitude = latitude;
        Longitude = longitude;
    }
}