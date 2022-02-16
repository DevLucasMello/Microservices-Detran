using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Core.Mediator;

namespace TP.Identidade.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
