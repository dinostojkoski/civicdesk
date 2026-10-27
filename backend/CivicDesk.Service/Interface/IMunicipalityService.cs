using CivicDesk.Service.Dtos;

namespace CivicDesk.Service.Interface;

public interface IMunicipalityService
{
    Task<List<MunicipalityDto>> GetByCityIdAsync(Guid cityId);
}