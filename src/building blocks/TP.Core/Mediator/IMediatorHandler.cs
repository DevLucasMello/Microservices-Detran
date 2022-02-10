using FluentValidation.Results;
using System.Threading.Tasks;
using TP.Core.Messages;

namespace TP.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
