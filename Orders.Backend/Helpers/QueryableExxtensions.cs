using Orders.Shared.DTOs;
using System.Linq;

namespace Orders.Backend.Helpers
{
    public static class QueryableExxtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTOs pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsNumber)
                .Take(pagination.RecordsNumber);
        }
    }
}
