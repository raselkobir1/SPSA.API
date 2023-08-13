using SPSA.API.Domain.Dtos.Common;
using System.Linq.Expressions;

namespace SPSA.API.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity); 
        void Update(T entity);  
        Task AddRange(IEnumerable<T> entities); 
        Task<T> GetById(long id);
        Task<IEnumerable<T>> GetAll();
        Task<TResult> Max<TResult>(Expression<Func<T, TResult>> where);
        Task<T>GetWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<IdExistsResponseDto> DoesIdsExistInDatabase(List<long> ids, Expression<Func<T, bool>> filter = null);
    }
}
