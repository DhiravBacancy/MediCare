using System.Collections.Generic;
using System.Linq.Expressions;
using MediCare.Repository; // Assuming your repository is in this namespace
using MediCare_.DTOs;

namespace MediCare_.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate); // Dynamic condition filter
        Task<IEnumerable<T>> GetByMultipleConditionsAsync(List<Filter> filters); // Property-based filter
        Task<PagedResult<T>> GetPaginatedAsync(int pageNumber, int pageSize);    
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }


    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<PagedResult<T>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetByConditionAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetByMultipleConditionsAsync(List<Filter> filters)
        {
            return await _repository.GetByMultipleConditionsAsync(filters);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
