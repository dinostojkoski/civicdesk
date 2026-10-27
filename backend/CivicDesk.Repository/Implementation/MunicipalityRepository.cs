using CivicDesk.Domain.Models;
using CivicDesk.Repository.Context;
using CivicDesk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CivicDesk.Repository.Implementation;

public class MunicipalityRepository
    : GenericRepository<Municipality>, IMunicipalityRepository
{
    public MunicipalityRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Municipality>> GetByCityIdAsync(Guid cityId)
    {
        return await _entities
            .Where(municipality => municipality.CityId == cityId)
            .OrderBy(municipality => municipality.Name)
            .ToListAsync();
    }
}