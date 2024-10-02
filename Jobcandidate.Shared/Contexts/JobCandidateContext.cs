using Jobcandidate.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Shared;

public class JobCandidateContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }

    public JobCandidateContext()
    { }

    public JobCandidateContext(DbContextOptions<JobCandidateContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
