using BlossomAvenue.Core.Users;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.Repositories.Cities;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Cities
{
    public class CityRepository : ICityRepository
    {
        private readonly BlossomAvenueDbContext _context;
        public CityRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<City?> GetCity(Guid cityId)
        {
            return await _context.Cities.FindAsync(cityId);
        }

        public async Task<bool> IsCityExists(Guid cityId)
        {
            return await _context.Cities.AnyAsync(c => c.CityId == cityId);
        }
    }
}
