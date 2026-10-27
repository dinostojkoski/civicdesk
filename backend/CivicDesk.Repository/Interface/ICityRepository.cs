using CivicDesk.Domain.Models;

namespace CivicDesk.Repository.Interface;

public interface ICityRepository : IGenericRepository<City>
{
    Task<List<City>> GetAllAsync();
}