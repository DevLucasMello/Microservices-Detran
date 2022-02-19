using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TP.Condutores.Application.Events
{
    public class CondutorEventHandler : INotificationHandler<CondutorRegistradoEvent>
    {
        public Task Handle(CondutorRegistradoEvent message, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}