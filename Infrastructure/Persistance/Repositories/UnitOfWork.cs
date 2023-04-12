using Application.Contracts.Persistance;
using System.Data.Common;

namespace Template.Infrastructure.Persistance.Dapper.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbTransaction? _transaction;
        private readonly DbConnection _connection;
        private bool _disposed = false;

        public UnitOfWork(DbConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public DbConnection Connection => _connection;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }

            }
            catch (Exception)
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
