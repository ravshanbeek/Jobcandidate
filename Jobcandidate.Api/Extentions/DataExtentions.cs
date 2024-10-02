using Jobcandidate.Application;
using Jobcandidate.Shared;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Api.Extensions
{
    public static class DataExtensions
    {
        // This extension method adds the DbContext to the service collection using a connection string from configuration
        public static IServiceCollection AddJobCandidateDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobCandidateContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Jobcandidate")));

            return services;
        }

        // This extension method registers the repositories and the UnitOfWork with scoped lifetime
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        // This extension method registers the application services (like CandidateService)
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddAutoMapper(typeof(MapperProfiler));

            return services;
        }
    }
}
