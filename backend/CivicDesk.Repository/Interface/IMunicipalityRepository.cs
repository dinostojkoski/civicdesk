using CivicDesk.Domain.Models;

namespace CivicDesk.Repository.Interface;

public interface IMunicipalityRepository 
    : IGenericRepository<Municipality>
{
    Task<List<Municipality>> GetByCityIdAsync(Guid cityId);
}