using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectTest.Data;

namespace ProjectTest.Configuration
{
    public static class DataConfig
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectTestContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
