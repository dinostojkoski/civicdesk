using CivicDesk.Service.Dtos;
using CivicDesk.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CivicDesk.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;
    private readonly IMunicipalityService _municipalityService;

    public CitiesController(
        ICityService cityService, 
        IMunicipalityService municipalityService)
    {
        _cityService = cityService;
        _municipalityService = municipalityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetAll()
    {
        var cities = await _cityService.GetAllAsync();

        return Ok(cities);
    }

    [HttpGet("{cityId:guid}/municipalities")]
    public async Task<ActionResult<List<MunicipalityDto>>> GetMunicipalities([FromRoute] Guid cityId)
    {
        var municipalities = await _municipalityService.GetByCityIdAsync(cityId);

        return Ok(municipalities);
    }
}