using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        // Get a list of entities
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties);

        // Get one single entity that matches the passed filter
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties);

        // Return a boolean result based on whether or not a dbset contains an entity that matches passed filter
        Task<bool> CheckIfExists(Expression<Func<T, bool>> filter);

        // Add a new passed entity to dbset
        Task<T> CreateAsync(T entity);

        // Remove a passed entity from existing dbset
        Task<T> DeleteAsync(T entity);

        // Get n number of rows from position m
        Task<IEnumerable<T>> GetBetweenAsync(int startingRow, int numberOfRows, string? includeProperties);

        // Get count
        Task<int> GetCountAsync(Expression<Func<T, bool>>? filter);

    }
}
