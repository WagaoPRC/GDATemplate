using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GDATemplate.Data.Context;
using GDATemplate.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GDATemplate.Data
{
    public class BaseRepository<TEntity, T> : IBaseRepository<TEntity> where TEntity : class where T : DbContext
    {
        protected readonly T _dbContext;

        protected DbSet<TEntity> DbSet { get; set; }

        public BaseRepository(T dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public async virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbSet.AsNoTracking().FirstAsync(predicate);

        public virtual TEntity GetById(object id)
        {
            var entity = _dbContext.Find<TEntity>(id);

            if (entity == null)
                return null;

            _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async virtual Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);
            if (entity == null)
                return null;

            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
            => DbSet.AsNoTracking();

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                var dbSet = DbSet.AsNoTracking().Where(predicate);
                if (includes != null)
                {
                    var query = dbSet.Include(includes[0]);
                    for (int i = 1; i < includes.Length; i++)
                    {
                        query = query.Include(includes[i]);
                    }
                    return query;
                }
                return dbSet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
            => await DbSet.AsNoTracking().ToListAsync();

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbSet.Where(predicate).AsNoTracking().ToListAsync();

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            foreach (var include in includes)
            {
                await _dbContext.Entry(dbSet).Reference(include.Name).LoadAsync();
            }
            return dbSet;
        }

        public async Task<TEntity> GetCompleteByIdAsync(object id)
        {
            await Task.Run(() => { });
            throw new NotImplementedException();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllCompleteAsync()
            => await DbSet.ToListAsync();

        public virtual void Update(TEntity entidade)
            => _dbContext.Update(entidade);

        public virtual async Task UpdateEntities(TEntity entidade)
            => _dbContext.Entry(entidade).State = EntityState.Modified;

        public virtual void Add(TEntity entidade)
            => _dbContext.Add(entidade);

        public async virtual Task AddAsync(TEntity entidade)
            => await _dbContext.AddAsync(entidade);

        public void AddAll(IEnumerable<TEntity> entidades)
            => _dbContext.AddRange(entidades);

        public async Task AddAllAsync(IEnumerable<TEntity> entidades)
           => await _dbContext.AddRangeAsync(entidades);

        public virtual void Delete(object id)
            => _dbContext.Remove(GetById(id));

        public async virtual Task DeleteAsync(object id)
            => _dbContext.Remove(await GetByIdAsync(id));

        public virtual void DeleteAll(IEnumerable<TEntity> entidades)
            => _dbContext.RemoveRange(entidades);

        public virtual List<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func)
            => DbSet.Where(func).ToList();
    }
}
