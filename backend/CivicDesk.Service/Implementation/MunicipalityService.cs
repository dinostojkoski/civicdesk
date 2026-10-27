using CivicDesk.Repository.Interface;
using CivicDesk.Service.Dtos;
using CivicDesk.Service.Interface;

namespace CivicDesk.Service.Implementation;

public class MunicipalityService : IMunicipalityService
{
    private readonly IMunicipalityRepository _municipalityRepository;

    public MunicipalityService(IMunicipalityRepository municipalityRepository)
    {
        _municipalityRepository = municipalityRepository;
    }

    public async Task<List<MunicipalityDto>> GetByCityIdAsync(Guid cityId)
    {
        var municipalities = await 
            _municipalityRepository.GetByCityIdAsync(cityId);

        return municipalities
            .Select(municipality => new MunicipalityDto(
                municipality.Id,
                municipality.Name,
                municipality.CityId))
            .ToList();
    }
}