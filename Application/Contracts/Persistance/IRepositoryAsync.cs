
using Application.Models.Persistance;

namespace Application.Contracts.Persistance
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(long id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetFilteredAsync(QueryFilter filter);
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
