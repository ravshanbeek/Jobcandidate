using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Jobcandidate.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobCandidateContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(JobCandidateContext context,
            IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ICandidateRepository CandidateRepository { get => GetRepository<ICandidateRepository>(); }

        #region Transuctions

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await this._context.SaveChangesAsync();
        }

        public IDbContextTransaction? CurrentTransaction
            => _context.Database.CurrentTransaction;

        // Begin Transaction
        public IDbContextTransaction BeginTransaction()
            => _context.Database.BeginTransaction();

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.BeginTransactionAsync(cancellationToken);

        // Commit Transaction
        public void CommitTransaction()
        {
            try
            {
                _context.SaveChanges(); // Save any changes made within the transaction
                _context.Database.CommitTransaction(); // Commit the transaction
            }
            catch
            {
                RollbackTransaction(); // Rollback if any exception occurs
                throw;
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken); // Save any changes asynchronously
                await _context.Database.CommitTransactionAsync(cancellationToken); // Commit asynchronously
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken); // Rollback on failure
                throw;
            }
        }

        // Reset State
        public void ResetState()
        {
            _context.Database.CurrentTransaction?.Dispose(); // Dispose current transaction
            _context.ChangeTracker.Clear(); // Clear the DbContext's ChangeTracker (removes tracked entities)
        }

        public async Task ResetStateAsync(CancellationToken cancellationToken = default)
        {
            _context.Database.CurrentTransaction?.Dispose(); // Dispose the transaction
            _context.ChangeTracker.Clear(); // Clear tracked entities
            await Task.CompletedTask; // Placeholder for async, since there's no actual async operation here
        }

        // Rollback Transaction
        public void RollbackTransaction()
        {
            if (CurrentTransaction != null)
            {
                _context.Database.RollbackTransaction(); // Rollback the current transaction
                ResetState(); // Reset the state of the context
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (CurrentTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken); // Rollback asynchronously
                await ResetStateAsync(cancellationToken); // Reset state asynchronously
            }
        }

#endregion

        #region Helpers
        public TRepository GetRepository<TRepository>()
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }
        #endregion

    }
}
