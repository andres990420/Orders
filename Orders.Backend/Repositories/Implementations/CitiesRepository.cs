using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTOs pagination)
        {
            var queryable = _context.Cities
                .Where(c => c.State!.Id == pagination.Id)
                .AsQueryable();

            return new ActionResponse<IEnumerable<City>>
            {
                WasSucceess = true,
                Result = await queryable
                    .OrderBy(c => c.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTOs pagination)
        {
            var queryable = _context.Cities
                .Where(c => c.State!.Id == pagination.Id)
                .AsQueryable();

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSucceess = true,
                Result = totalPages
            };
        }
    }
}