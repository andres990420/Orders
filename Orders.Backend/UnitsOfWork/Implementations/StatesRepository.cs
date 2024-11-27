using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Implementations;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class StatesRepository : GenericRepository<State> ,IStatesRepository
    {
        private readonly DataContext _context;

        public StatesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<State>> GetAsync(int id)
        {
            var state = await _context.States
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (state == null)
            {
                return new ActionResponse<State>
                {
                    WasSucceess = false,
                    Message = "Estado no existe"
                };
            }

            return new ActionResponse<State>
            {
                WasSucceess = true,
                Result = state
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States
                .Include(c => c.Cities)
                .ToListAsync();
            return new ActionResponse<IEnumerable<State>>
            {
                WasSucceess = true,
                Result = states
            };
        }
    }
}
