using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GDATemplate.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetCompleteByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllCompleteAsync();

        void Update(TEntity entidade);
        Task UpdateEntities(TEntity entidade);

        void Add(TEntity entidade);
        Task AddAsync(TEntity entidade);
        void AddAll(IEnumerable<TEntity> entidades);
        Task AddAllAsync(IEnumerable<TEntity> entidades);

        void Delete(object id);
        Task DeleteAsync(object id);
        void DeleteAll(IEnumerable<TEntity> entidades);

        List<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func);
    }
}
