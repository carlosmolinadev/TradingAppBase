using System.Data.Common;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
