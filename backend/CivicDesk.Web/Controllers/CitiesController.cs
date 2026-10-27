using CivicDesk.Service.Dtos;
using CivicDesk.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CivicDesk.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetAll()
    {
        var cities = await _cityService.GetAllAsync();

        return Ok(cities);
    }
}