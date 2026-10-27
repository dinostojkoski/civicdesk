using CivicDesk.Domain.Models;
using CivicDesk.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace CivicDesk.Repository.Seed;

public static class LocationSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var skopje = await context.Cities
            .FirstOrDefaultAsync(city => city.Name == "Skopje");

        if (skopje is null)
        {
            skopje = new City("Skopje");
            await context.Cities.AddAsync(skopje);
        }

        var prilep = await context.Cities
            .FirstOrDefaultAsync(city => city.Name == "Prilep");

        if (prilep is null)
        {
            prilep = new City("Prilep");
            await context.Cities.AddAsync(prilep);
        }

        await AddMunicipalityIfMissingAsync(context, "Karpoš", skopje.Id);
        await AddMunicipalityIfMissingAsync(context, "Centar", skopje.Id);
        await AddMunicipalityIfMissingAsync(context, "Prilep", prilep.Id);

        await context.SaveChangesAsync();
    }

    private static async Task AddMunicipalityIfMissingAsync(
        ApplicationDbContext context,
        string name,
        Guid cityId)
    {
        var exists = await context.Municipalities
            .AnyAsync(municipality =>
                municipality.Name == name &&
                municipality.CityId == cityId);

        if (!exists)
        {
            await context.Municipalities.AddAsync(
                new Municipality(name, cityId));
        }
    }
}