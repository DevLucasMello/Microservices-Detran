﻿using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TP.Core.Messages;
using TP.Core.Messages.Integration;
using TP.MessageBus;
using TP.Veiculos.Application.Events;
using TP.Veiculos.Application.Messages;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Application.Commands
{
    public class VeiculoCommandHandler : CommandHandler,
        IRequestHandler<AdicionarVeiculoCommand, ValidationResult>,
        IRequestHandler<AtualizarVeiculoCommand, ValidationResult>,
        IRequestHandler<ExcluirVeiculoCommand, ValidationResult>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMessageBus _bus;

        public VeiculoCommandHandler(IVeiculoRepository veiculoRepository, IMessageBus bus)
        {
            _veiculoRepository = veiculoRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(AdicionarVeiculoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var veiculos = await _veiculoRepository.ObterVeiculosPorCPF(50,1,message.CPF);

            if (veiculos.List.Any())
            {
                var check = false;
                veiculos.List.ToList().ForEach(v =>
                {
                    if (v.Placa == message.Placa)
                    {
                        check = true;
                        AdicionarErro(VeiculoCommandErrorMessages.PlacaCadastradaErroMsg);
                    }
                });

                if (check) return ValidationResult;
            }

            var veiculo = new Veiculo(message.Placa, message.Modelo, message.Marca, message.Cor, message.AnoFabricacao);

            _veiculoRepository.Adicionar(veiculo, message.CondutorId.ToString(), message.CPF);

            veiculo.AdicionarEvento(new VeiculoCadastradoEvent(veiculo.Id, message.CondutorId, message.Placa));

            var result = await PersistirDados(_veiculoRepository.UnitOfWork);

            if (!(result.Errors.Count > 0))
            {
                var condutorIntegration = new AtualizarVeiculoCondutorIntegrationEvent(veiculo.Id, message.CondutorId, message.Placa);
                await _bus.RequestAsync<AtualizarVeiculoCondutorIntegrationEvent, ResponseMessage>(condutorIntegration);
            }

            return result;
        }

        public async Task<ValidationResult> Handle(AtualizarVeiculoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var veiculoExistente = await _veiculoRepository.ObterPorId(message.Id);

            if (veiculoExistente == null)
            {
                AdicionarErro(VeiculoCommandErrorMessages.VeiculoNaoEncontradoErroMsg);
                return ValidationResult;
            }

            var veiculo = new Veiculo(veiculoExistente.Placa, message.Modelo, message.Marca, message.Cor, message.AnoFabricacao)
            {
                Id = message.Id
            };

            _veiculoRepository.Atualizar(veiculo);

            return await PersistirDados(_veiculoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(ExcluirVeiculoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var veiculo = await _veiculoRepository.ObterPorId(message.Id);

            if (veiculo == null)
            {
                AdicionarErro(VeiculoCommandErrorMessages.VeiculoNaoEncontradoErroMsg);
                return ValidationResult;
            }

            _veiculoRepository.Excluir(veiculo);

            var result = await PersistirDados(_veiculoRepository.UnitOfWork);

            if (!(result.Errors.Count > 0))
            {
                var condutorIntegration = new RemoverVeiculoCondutorIntegrationEvent(message.Id, veiculo.Placa);
                await _bus.RequestAsync<RemoverVeiculoCondutorIntegrationEvent, ResponseMessage>(condutorIntegration);
            }

            return result;
        }
    }
}