using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GDATemplate.Application.Interfaces;
using GDATemplate.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GDATemplate.Application
{
    public class BaseService<T, TEntity> : IBaseService<T, TEntity> where T : class where TEntity : class
    {
        public IBaseRepository<TEntity> _repository;
        public IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<T> GetCompleteByIdAsync(object id)
            => _mapper.Map<T>(await _repository.GetCompleteByIdAsync(id));

        public T GetById(object id)
            => _mapper.Map<T>(_repository.GetById(id));

        public async Task<T> GetByIdAsync(object id)
            => _mapper.Map<T>(await _repository.GetByIdAsync(id));

        public IEnumerable<T> GetAll()
            => _mapper.Map<IEnumerable<T>>(_repository.GetAll());

        public async Task<IEnumerable<T>> GetAllAsync()
            => _mapper.Map<IEnumerable<T>>(await _repository.GetAllAsync());

        public async Task<IEnumerable<T>> GetAllCompleteAsync()
            => _mapper.Map<IEnumerable<T>>(await _repository.GetAllCompleteAsync());

        public void Update(T dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);
            _repository.Update(_mapper.Map<TEntity>(dto));
        }

        public T Add(T dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);

            var entidade = _mapper.Map<TEntity>(dto);
            _repository.Add(entidade);
            return _mapper.Map<T>(entidade);
        }

        public async Task<T> AddAsync(T dto)
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);

            var entidade = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entidade);
            return _mapper.Map<T>(entidade);
        }

        public IEnumerable<T> AddAll(IEnumerable<T> dtos)
        {
            Validator.ValidateObject(dtos, new ValidationContext(dtos), true);

            var entidades = _mapper.Map<IEnumerable<TEntity>>(dtos);
            _repository.AddAll(entidades);
            return _mapper.Map<IEnumerable<T>>(entidades);
        }

        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> dtos)
        {
            Validator.ValidateObject(dtos, new ValidationContext(dtos), true);

            var entidades = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _repository.AddAllAsync(entidades);
            return _mapper.Map<IEnumerable<T>>(entidades);
        }

        public void Delete(object id)
            => _repository.Delete(id);


        public async Task DeleteAsync(object id)
            => await _repository.DeleteAsync(id);


        public void DeleteAll(IEnumerable<T> entidades)
            => _repository.DeleteAll(_mapper.Map<IEnumerable<TEntity>>(entidades));


    }
}
