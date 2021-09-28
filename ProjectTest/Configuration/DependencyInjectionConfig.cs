using Microsoft.Extensions.DependencyInjection;
using ProjectTest.Data.Repositories;
using ProjectTest.Data.Repositories.Interfaces;
using ProjectTest.Services;
using ProjectTest.Services.Interfaces;

namespace ProjectTest.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddTransient<ISpStoreUserDataRepository, SpStoreUserDataRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUsersAuthRepository, UsersAuthRepository>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
