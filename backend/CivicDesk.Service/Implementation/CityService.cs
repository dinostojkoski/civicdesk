using CivicDesk.Repository.Interface;
using CivicDesk.Service.Dtos;
using CivicDesk.Service.Interface;

namespace CivicDesk.Service.Implementation;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<List<CityDto>> GetAllAsync()
    {
        var cities = await _cityRepository.GetAllAsync();

        return cities.Select(city => new CityDto(city.Id, city.Name)).ToList();
    }
}