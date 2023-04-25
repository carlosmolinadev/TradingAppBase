
using Application.Models.Persistance;

namespace Application.Contracts.Persistance
{
    public interface IRepositoryAsync<T> where T : class 
    {
        Task<T> SelectByIdAsync(int id);
        Task<ICollection<T>> SelectAllAsync();
        Task<ICollection<T>> SelectFilteredAsync(QueryFilter filter);
        Task<T> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task SaveAsync<P>(string storedProcedure, P parameters);
        Task<ICollection<U>> LoadAsync<U, P>(string storedProcedure, P parameters);
    }
}
