using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TP.Core.Mediator;
using TP.Core.Messages.Integration;
using TP.MessageBus;
using TP.Veiculos.Application.Commands;

namespace TP.Veiculos.Application.Services
{
    public class RemoverCondutorVeiculoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RemoverCondutorVeiculoIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RemoverCondutorVeiculoIntegrationEvent, ResponseMessage>(async request =>
                await AtualizarVeiculoCondutor(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> AtualizarVeiculoCondutor(RemoverCondutorVeiculoIntegrationEvent message)
        {
            var veiculoCommand = new ExcluirCondutorVeiculoCommand(message.VeiculoId, message.CondutorId, message.CPF);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(veiculoCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
