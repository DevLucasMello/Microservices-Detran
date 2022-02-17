using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Events;
using TP.Condutores.Domain;
using TP.Condutores.Infra.Data;
using TP.Condutores.Infra.Data.Repository;
using TP.Core.Mediator;

namespace TP.Identidade.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Config Context
            services.AddDbContext<CondutoresContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Commands
            services.AddScoped<IRequestHandler<AdicionarCondutorCommand, ValidationResult>, CondutorCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<CondutorRegistradoEvent>, CondutorEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Data
            services.AddScoped<ICondutorRepository, CondutorRepository>();
            services.AddScoped<CondutoresContext>();
        }
    }
}
