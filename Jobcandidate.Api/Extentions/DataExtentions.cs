using Jobcandidate.Application;
using Jobcandidate.Shared;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Api.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddJobCandidateDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobCandidateContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Jobcandidate")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IManualService, ManualService>();
            services.AddAutoMapper(typeof(MapperProfiler));

            return services;
        }
    }
}
