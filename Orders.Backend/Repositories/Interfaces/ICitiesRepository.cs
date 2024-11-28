using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTOs pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTOs pagination);
    }
}
