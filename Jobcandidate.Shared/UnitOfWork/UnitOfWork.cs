using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Jobcandidate.Shared
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly JobCandidateContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(JobCandidateContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        #region Repositories
        public ICandidateRepository CandidateRepository => GetRepository<ICandidateRepository>();

        #endregion

        #region Transactions
        public IDbContextTransaction? CurrentTransaction => _context.Database.CurrentTransaction;

        public IDbConnection? Connection => throw new NotImplementedException();

        public IsolationLevel IsolationLevel => throw new NotImplementedException();

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _context.Database.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (CurrentTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);
                ResetState();
            }
        }

        private void ResetState()
        {
            _context.ChangeTracker.Clear();
        }

        #endregion

        #region Helpers
        public TRepository GetRepository<TRepository>()
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
