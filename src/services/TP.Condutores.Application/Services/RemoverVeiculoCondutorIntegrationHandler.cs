using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TP.Condutores.Application.Commands;
using TP.Core.Mediator;
using TP.Core.Messages.Integration;
using TP.MessageBus;

namespace TP.Condutores.Application.Services
{
    public class RemoverVeiculoCondutorIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RemoverVeiculoCondutorIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RemoverVeiculoCondutorIntegrationEvent, ResponseMessage>(async request =>
                await RemoverVeiculoCondutor(request));

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

        private async Task<ResponseMessage> RemoverVeiculoCondutor(RemoverVeiculoCondutorIntegrationEvent message)
        {
            var condutorCommand = new ExcluirVeiculoCondutorCommand(message.CondutorId, message.VeiculoId, message.Placa);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(condutorCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
