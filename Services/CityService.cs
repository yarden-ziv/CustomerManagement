using CustomerManagement.Data;
using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Services
{
    public class CityService
    {
        private readonly AppDbContext _context;

        public CityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }
    }
}