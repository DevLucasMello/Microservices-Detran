using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Veiculos.Application.Commands
{
    public class AtualizarVeiculoCommand : Command
    {
        public Guid Id { get; set; }
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Marca { get; private set; }
        public string Cor { get; private set; }
        public int AnoFabricacao { get; private set; }

        public AtualizarVeiculoCommand(Guid id, string placa, string modelo, string marca, string cor, int anoFabricacao)
        {
            Id = id;
            AggregateId = id;
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Cor = cor;
            AnoFabricacao = anoFabricacao;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarVeiculoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarVeiculoValidation : AbstractValidator<AtualizarVeiculoCommand>
        {
            public AtualizarVeiculoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do condutor inválido");

                RuleFor(v => v.Placa)
                    .NotEmpty()
                    .WithMessage("A Placa deve ser informada")
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage("A Placa informada é inválida");

                RuleFor(v => v.Modelo)
                    .NotEmpty()
                    .WithMessage("O Modelo deve ser informado");

                RuleFor(s => s.Marca)
                    .NotEmpty()
                    .WithMessage("O Email não foi informado");

                RuleFor(c => c.Cor)
                    .NotEmpty()
                    .WithMessage("A CNH deve ser informada");

                RuleFor(c => c.AnoFabricacao)
                    .NotEmpty()
                    .WithMessage("O Ano de Fabricação deve ser informado");
            }
        }
    }
}
