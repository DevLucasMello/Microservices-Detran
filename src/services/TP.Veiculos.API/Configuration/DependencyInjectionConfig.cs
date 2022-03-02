using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Core.Mediator;
using TP.Veiculos.Domain;
using TP.Veiculos.Infra.Data;
using TP.Veiculos.Infra.Data.Repository;

namespace TP.Veiculos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Config Context
            services.AddDbContext<VeiculosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Commands

            // Events

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Data
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<VeiculosContext>();

            // AutoMapper
        }
    }
}
