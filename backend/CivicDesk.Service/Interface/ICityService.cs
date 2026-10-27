using CivicDesk.Service.Dtos;

namespace CivicDesk.Service.Interface;

public interface ICityService
{
    Task<List<CityDto>> GetAllAsync();
}