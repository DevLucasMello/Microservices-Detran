using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Condutores.Application.Commands
{
    public class ExcluirVeiculoCondutorCommand : Command
    {
        public Guid VeiculoId { get; private set; }
        public string Placa { get; private set; }

        public ExcluirVeiculoCondutorCommand(Guid veiculoId, string placa)
        {
            VeiculoId = veiculoId;
            Placa = placa;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirVeiculoCondutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirVeiculoCondutorValidation : AbstractValidator<ExcluirVeiculoCondutorCommand>
        {
            public ExcluirVeiculoCondutorValidation()
            {
                RuleFor(c => c.VeiculoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do veículo inválido");

                RuleFor(c => c.Placa)
                    .NotEmpty()
                    .WithMessage("A placa deve ser informada")
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage("A placa informada é inválida");
            }
        }
    }
}