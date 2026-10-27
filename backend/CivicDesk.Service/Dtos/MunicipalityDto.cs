namespace CivicDesk.Service.Dtos;

public record MunicipalityDto(Guid Id, string Name, Guid? CityId);