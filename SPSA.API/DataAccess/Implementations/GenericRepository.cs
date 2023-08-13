using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain.Dtos.Common;
using System.Linq.Expressions;

namespace SPSA.API.DataAccess.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
             _dbContext.Set<T>().Update(entity);
        }
        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if(include != null && include.Count() > 0)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            return await query.Where(where).FirstOrDefaultAsync();  
        }

        public async Task<TResult> Max<TResult>(Expression<Func<T, TResult>> where)
        {
            return await _dbContext.Set<T>().MaxAsync(where);
        }

        public async Task<IdExistsResponseDto> DoesIdsExistInDatabase(List<long> ids, Expression<Func<T, bool>> filter = null)
        {
            var existingIdsQuery = _dbContext.Set<T>();
            if(filter != null)
                existingIdsQuery.Where(filter);

            var existingIds = await existingIdsQuery
                .Select(GetIdExpression<T>())
                .ToListAsync();

            var missingIds = ids.Except(existingIds).ToList();

            var response = new IdExistsResponseDto()
            {
                DoesAllIdExists = (missingIds.Count == 0),
                NotExistsList = new List<long>(missingIds)
            };

            return response;
        }

        public async Task<(bool, string)> HasDependency(string table, string id)
        {
            var data = new List<HasDependencyDto>();
            await _dbContext.LoadStoredProc("CheckDependency")
                .WithSqlParam("TableName", table)    
                .WithSqlParam("id", id)
                .ExecuteStoredProcAsync((handler) =>
                {
                     data = handler.ReadToList<HasDependencyDto>().ToList();
                });

            return (true, data.FirstOrDefault().DependenceMessage);
        }

        #region Private Methods
        private Expression<Func<T, long>> GetIdExpression<T>()
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, "Id");
            var lambda = Expression.Lambda<Func<T, long>>(property, parameter);

            return lambda;
        }

        #endregion
    }
}
