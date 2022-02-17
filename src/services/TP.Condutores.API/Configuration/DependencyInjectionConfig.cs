using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Condutores.Infra.Data;
using TP.Core.Mediator;

namespace TP.Identidade.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CondutoresContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
