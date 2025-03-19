using MediCare.Data;
using MediCare.Models;
using MediCare_.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediCare.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate); 
        Task<IEnumerable<T>> GetByMultipleConditionsAsync(List<Filter> filters);
        Task<PagedResult<T>> GetPaginatedAsync(int pageNumber, int pageSize);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        // 🔹 GetByConditionAsync - Uses a Predicate (Lambda Expression)
        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByMultipleConditionsAsync(List<Filter> filters)
        {
            // If no filters are provided, return all records
            if (filters == null || !filters.Any())
            {
                return await _context.Set<T>().ToListAsync();
            }

            var entityType = typeof(T);
            var parameter = Expression.Parameter(typeof(T), "x");

            Expression combinedExpression = null;

            foreach (var filter in filters)
            {
                var property = entityType.GetProperty(filter.PropertyName);
                if (property == null)
                    throw new ArgumentException($"Property '{filter.PropertyName}' does not exist on '{entityType.Name}'");

                var propertyAccess = Expression.Property(parameter, property);

                Expression filterExpression = null;

                // Check for the type of the property and build the filter accordingly
                if (property.PropertyType == typeof(string))
                {
                    var constantValue = Expression.Constant(filter.Value);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    filterExpression = Expression.Call(propertyAccess, containsMethod, constantValue);
                }
                else if (property.PropertyType == typeof(int))
                {
                    if (int.TryParse(filter.Value, out var intValue))
                    {
                        var constantValue = Expression.Constant(intValue);
                        filterExpression = Expression.Equal(propertyAccess, constantValue);
                    }
                    else
                    {
                        throw new ArgumentException($"The value '{filter.Value}' could not be converted to type 'int'.");
                    }
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    if (DateTime.TryParse(filter.Value, out var dateValue))
                    {
                        var constantValue = Expression.Constant(dateValue);
                        filterExpression = Expression.Equal(propertyAccess, constantValue);
                    }
                    else
                    {
                        throw new ArgumentException($"The value '{filter.Value}' could not be converted to type 'DateTime'.");
                    }
                }
                // Add more types as needed.

                // Combine the expressions using 'AND' (&&)
                if (combinedExpression == null)
                {
                    combinedExpression = filterExpression;
                }
                else
                {
                    combinedExpression = Expression.AndAlso(combinedExpression, filterExpression);
                }
            }

            // Build the lambda expression
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

            return await _context.Set<T>().Where(lambda).ToListAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    public async Task<PagedResult<T>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Set<T>().AsQueryable();

            var totalRecords = await query.CountAsync();
            var pagedItems = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = pagedItems,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

