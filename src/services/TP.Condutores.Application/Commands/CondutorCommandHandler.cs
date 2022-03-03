using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TP.Condutores.Application.Events;
using TP.Condutores.Domain;
using TP.Core.Messages;

namespace TP.Condutores.Application.Commands
{
    public class CondutorCommandHandler : CommandHandler,
        IRequestHandler<AdicionarCondutorCommand, ValidationResult>,
        IRequestHandler<AtualizarCondutorCommand, ValidationResult>,
        IRequestHandler<AtualizarVeiculoCondutorCommand, ValidationResult>,
        IRequestHandler<ExcluirCondutorCommand, ValidationResult>,
        IRequestHandler<ExcluirVeiculoCondutorCommand, ValidationResult>
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

        public async Task<ValidationResult> Handle(AtualizarCondutorCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var condutorExistente = await _condutorRepository.ObterPorId(message.Id);

            if (condutorExistente == null)
            {
                AdicionarErro("Condutor não encontrado.");
                return ValidationResult;
            }

            var condutor = new Condutor(message.Nome, condutorExistente.CPF, message.Telefone, message.Email, message.CNH, message.DataNascimento)
            {
                Id = message.Id
            };

            _condutorRepository.Atualizar(condutor);

            return await PersistirDados(_condutorRepository.UnitOfWork);
        }
        
        public async Task<ValidationResult> Handle(AtualizarVeiculoCondutorCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var condutorExistente = await _condutorRepository.ObterPorId(message.CondutorId);

            if (condutorExistente == null)
            {
                AdicionarErro("Condutor não encontrado.");
                return ValidationResult;
            }

            if (condutorExistente.Veiculo == null)
            {
                var condutor = new Condutor(condutorExistente.Nome, condutorExistente.CPF, condutorExistente.Telefone, condutorExistente.Email, condutorExistente.CNH, condutorExistente.DataNascimento)
                {
                    Id = condutorExistente.Id
                };

                return await AtualizarCondutorVeiculo(condutor, message.VeiculoId, message.Placa);
            }
           
            return await AtualizarCondutorVeiculo(condutorExistente, message.VeiculoId, message.Placa);            
        }

        public async Task<ValidationResult> Handle(ExcluirCondutorCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var condutor = await _condutorRepository.ObterPorId(message.Id);

            if (condutor == null)
            {
                AdicionarErro("Condutor não encontrado.");
                return ValidationResult;
            }

            if (condutor.Veiculo != null)
            {
                AdicionarErro("Necessário excluir os veículos cadastrados do condutor antes de excluí-lo.");
                return ValidationResult;
            }

            _condutorRepository.Excluir(condutor);

            return await PersistirDados(_condutorRepository.UnitOfWork);
        }
       
        public async Task<ValidationResult> Handle(ExcluirVeiculoCondutorCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var veiculo = await _condutorRepository.ObterVeiculoId(message.VeiculoId);

            if (veiculo == null)
            {
                AdicionarErro("Veículo não encontrado.");
                return ValidationResult;
            }

            var condutor = await _condutorRepository.ObterPorId(veiculo.CondutorId);

            if (condutor == null)
            {
                AdicionarErro("Condutor não encontrado.");
                return ValidationResult;
            }

            condutor.RemoverVeiculo(veiculo, condutor);

            return await PersistirDados(_condutorRepository.UnitOfWork);
        }

        private async Task<ValidationResult> AtualizarCondutorVeiculo(Condutor condutor, Guid veiculoId, string placa)
        {
            _condutorRepository.Atualizar(condutor, veiculoId, placa);

            return await PersistirDados(_condutorRepository.UnitOfWork);
        }
    }
}