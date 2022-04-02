using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Veiculos.Application.Messages;

namespace TP.Veiculos.Application.Commands
{
    public class ExcluirVeiculoCommand : Command
    {
        public Guid Id { get; set; }

        public ExcluirVeiculoCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirVeiculoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirVeiculoValidation : AbstractValidator<ExcluirVeiculoCommand>
        {
            public ExcluirVeiculoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage(VeiculoCommandErrorMessages.IdNuloErroMsg);
            }
        }
    }
}
