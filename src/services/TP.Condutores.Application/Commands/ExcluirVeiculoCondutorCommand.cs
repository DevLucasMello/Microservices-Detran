using FluentValidation;
using System;
using TP.Condutores.Application.Messages;
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
                    .WithMessage(CondutorCommandErrorMessages.VeiculoIdNuloErroMsg);

                RuleFor(c => c.Placa)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.PlacaNuloErroMsg)
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage(CondutorCommandErrorMessages.PlacaInvalidaErroMsg);
            }
        }
    }
}