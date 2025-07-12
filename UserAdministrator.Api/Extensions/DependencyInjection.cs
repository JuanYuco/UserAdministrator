using UserAdministrator.Api.Services;
using UserAdministrator.Api.Services.Interfaces;
using UserAdministrator.Data.Interfaces;
using UserAdministrator.Data.Repositories;

namespace UserAdministrator.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddSingleton<IAppContext, UserAdministrator.Data.DBContext.AppContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
