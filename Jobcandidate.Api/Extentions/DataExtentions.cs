using Jobcandidate.Shared;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Api.Extentions;

public static class DataExtentions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<JobCandidateContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("Jobcandidate")));

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
