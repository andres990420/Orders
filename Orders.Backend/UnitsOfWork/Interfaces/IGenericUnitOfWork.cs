using Orders.Shared.DTOs;
using Orders.Shared.Reponses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class 
    {
        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTOs pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTOs pagination);

        Task<ActionResponse<T>> AddAsync(T entity);

        Task<ActionResponse<T>> DeleteAsync(int id);

        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}
