
using Application.Models.Persistance;

namespace Application.Contracts.Persistance
{
    public interface IRepositoryAsync<T> where T : class 
    {
        Task<T> SelectByIdAsync<Tid>(Tid id);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectByParameterAsync(QueryParameter queryParameter);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task SaveAsync<P>(string storedProcedure, P parameters);
        Task<IEnumerable<U>> LoadAsync<U, P>(string storedProcedure, P parameters);
    }
}
