using Jobcandidate.Shared;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Api.Extentions;

public static class DataExtentions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<JobCandidateContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("Jobcandidate")));
}
