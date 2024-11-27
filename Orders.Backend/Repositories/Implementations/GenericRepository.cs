using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Reponses;
using System.Data;

namespace Orders.Backend.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        
        
        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSucceess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception exception) 
            {
                return ExecptionActionResponse(exception);
            }    
            
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null) 
            {
                return new ActionResponse<T>
                {
                    WasSucceess = false,
                    Message = "Registro no encontrado"
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSucceess = true
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    WasSucceess = false,
                    Message = "No se puede borrar, porque tiene registros relacionados"
                };
                
            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<T>
                {
                    WasSucceess = false,
                    Message = "Registro no encontrado"
                };
            }
            return new ActionResponse<T>
            { 
                WasSucceess = true,
                Result = row
            };
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                WasSucceess = true,
                Result = await _entity.ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            _context.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSucceess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception exception)
            {
                return ExecptionActionResponse(exception);
            }

        }
        private ActionResponse<T> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<T>
            {
                WasSucceess = false,
                Message = "Ya existe el registro que estas intetando crear."
            };
        }
        private ActionResponse<T> ExecptionActionResponse(Exception exception)
        {
            return new ActionResponse<T>
            {
                WasSucceess = false,
                Message = exception.Message
            };
        }
    }
}
