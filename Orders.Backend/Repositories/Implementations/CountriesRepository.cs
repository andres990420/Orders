using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null) 
            {
                return new ActionResponse<Country>
                {
                    WasSucceess = false,
                    Message = "Pais no existe"
                };
            }

            return new ActionResponse<Country>
            {
                WasSucceess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .Include(c => c.States)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSucceess = true,
                Result = countries
            };
        }
    }
}
