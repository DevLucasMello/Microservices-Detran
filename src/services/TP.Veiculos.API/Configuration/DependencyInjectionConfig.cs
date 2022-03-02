using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP.Core.Mediator;
using TP.Veiculos.Application.AutoMapper;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Events;
using TP.Veiculos.Application.Queries;
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
            services.AddScoped<IRequestHandler<AdicionarVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirCondutorVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<VeiculoCadastradoEvent>, VeiculoEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVeiculoQueries, VeiculoQueries>();

            // Data
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<VeiculosContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(AdicionarVeiculoCommandToViewModel), typeof(ViewModelToAdicionarVaiculoCommand));
            services.AddAutoMapper(typeof(AtualizarVeiculoCommandToViewModel), typeof(ViewModelToAtualizarVeiculoCommand));
            services.AddAutoMapper(typeof(ExibirVeiculoQuerieToViewModel), typeof(ViewModelToExibirVeiculoQuerie));
        }
    }
}
