using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Jobcandidate.Shared;

public interface IUnitOfWork : IDbContextTransactionManager
{
    ICandidateRepository CandidateRepository { get; }
    Task SaveChangesAsync();
}
