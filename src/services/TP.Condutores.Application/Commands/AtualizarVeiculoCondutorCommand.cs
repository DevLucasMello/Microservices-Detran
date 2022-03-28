using FluentValidation;
using System;
using TP.Condutores.Application.Messages;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Condutores.Application.Commands
{
    public class AtualizarVeiculoCondutorCommand : Command
    {
        public Guid CondutorId { get; set; }        
        public Guid VeiculoId { get; private set; }
        public string Placa { get; private set; }

        public AtualizarVeiculoCondutorCommand(Guid condutorId, Guid veiculoId, string placa)
        {
            CondutorId = condutorId;
            AggregateId = condutorId;            
            VeiculoId = veiculoId;
            Placa = placa;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarVeiculoCondutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarVeiculoCondutorValidation : AbstractValidator<AtualizarVeiculoCondutorCommand>
        {
            public AtualizarVeiculoCondutorValidation()
            {
                RuleFor(c => c.CondutorId)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CondutorCommandErrorMessages.CondutorIdNuloErroMsg);

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