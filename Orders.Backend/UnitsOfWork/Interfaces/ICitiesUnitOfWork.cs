using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface ICitiesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTOs pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTOs pagination);
    }
}
