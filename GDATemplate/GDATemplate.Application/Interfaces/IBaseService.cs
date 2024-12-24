using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GDATemplate.Application.Interfaces
{
    public interface IBaseService<T, TEntity> where T : class where TEntity : class
    {
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetCompleteByIdAsync(object id);
        Task<IEnumerable<T>> GetAllCompleteAsync();
        void Update(T entidade);
        T Add(T entidade);
        Task<T> AddAsync(T entidade);
        IEnumerable<T> AddAll(IEnumerable<T> entidades);
        Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entidades);
        void Delete(object id);
        Task DeleteAsync(object id);
        void DeleteAll(IEnumerable<T> entidades);
    }
}
