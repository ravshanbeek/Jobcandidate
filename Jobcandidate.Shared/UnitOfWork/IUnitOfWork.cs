using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Jobcandidate.Shared;

public interface IUnitOfWork : IDbTransaction
{
    ICandidateRepository CandidateRepository { get; }
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
