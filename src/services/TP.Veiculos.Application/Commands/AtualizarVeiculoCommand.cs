using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;
using TP.Veiculos.Application.Messages;

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
                    .WithMessage(VeiculoCommandErrorMessages.IdNuloErroMsg);

                RuleFor(v => v.Placa)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.PlacaNuloErroMsg)
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage(VeiculoCommandErrorMessages.PlacaInvalidaErroMsg);

                RuleFor(v => v.Modelo)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.ModeloNuloErroMsg);

                RuleFor(s => s.Marca)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.MarcaNuloErroMsg);

                RuleFor(c => c.Cor)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.CorNuloErroMsg);

                RuleFor(c => c.AnoFabricacao)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.AnoFabricacaoNuloErroMsg);
            }
        }
    }
}
