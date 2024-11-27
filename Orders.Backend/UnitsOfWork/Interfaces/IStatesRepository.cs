using Orders.Shared.Entities;
using Orders.Shared.Reponses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface IStatesRepository
    {
       Task<ActionResponse<State>> GetAsync(int id);

       Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}
