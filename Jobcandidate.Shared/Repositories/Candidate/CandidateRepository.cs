using Jobcandidate.Domain;

namespace Jobcandidate.Shared;

public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
{
    public CandidateRepository(JobCandidateContext context) : base(context)
    {
    }
}
