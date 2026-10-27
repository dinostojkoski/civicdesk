using CivicDesk.Domain.Models;
using CivicDesk.Repository.Context;
using CivicDesk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CivicDesk.Repository.Implementation;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<City>> GetAllAsync()
    {
        return await _entities
            .OrderBy(city => city.Name)
            .ToListAsync();
    }
}