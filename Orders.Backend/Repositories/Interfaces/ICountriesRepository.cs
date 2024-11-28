using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTOs pagination);
    }
}
