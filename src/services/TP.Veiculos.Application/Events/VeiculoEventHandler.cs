using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TP.Veiculos.Application.Events
{
    public class VeiculoEventHandler : INotificationHandler<VeiculoCadastradoEvent>
    {
        public Task Handle(VeiculoCadastradoEvent message, CancellationToken cancellationToken)
        {
            // Realizar uma ação
            return Task.CompletedTask;
        }
    }
}
