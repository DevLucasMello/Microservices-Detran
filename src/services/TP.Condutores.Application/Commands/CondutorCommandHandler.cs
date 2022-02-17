using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TP.Condutores.Application.Events;
using TP.Condutores.Domain;
using TP.Core.Messages;

namespace TP.Condutores.Application.Commands
{
    public class CondutorCommandHandler : CommandHandler,
        IRequestHandler<AdicionarCondutorCommand, ValidationResult>
    {
        private readonly ICondutorRepository _condutorRepository;

        public CondutorCommandHandler(ICondutorRepository condutorRepository)
        {
            _condutorRepository = condutorRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarCondutorCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var condutor = new Condutor(message.Nome, message.CPF, message.Telefone, message.Email, message.CNH, message.DataNascimento);

            var condutorExistente = await _condutorRepository.ObterPorCPF(condutor.CPF);

            if (condutorExistente != null)
            {
                AdicionarErro("Este CPF já está em uso por outro condutor.");
                return ValidationResult;
            }

            _condutorRepository.Adicionar(condutor);

            condutor.AdicionarEvento(new CondutorRegistradoEvent(message.Nome, message.CPF, message.Email));

            return await PersistirDados(_condutorRepository.UnitOfWork);
        }
    }
}
