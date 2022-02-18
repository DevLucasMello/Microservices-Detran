using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TP.Condutores.Application.Events
{
    public class CondutorEventHandler : INotificationHandler<CondutorRegistradoEvent>,
                                        INotificationHandler<CondutorAtualizadoEvent>
    {
        public Task Handle(CondutorRegistradoEvent message, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }

        public Task Handle(CondutorAtualizadoEvent message, CancellationToken cancellationToken)
        {
            // Enviar evento de atualização condutor na API Veículo
            return Task.CompletedTask;
        }
    }
}