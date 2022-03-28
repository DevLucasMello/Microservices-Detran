using FluentValidation;
using System;
using TP.Condutores.Application.Messages;
using TP.Core.Messages;

namespace TP.Condutores.Application.Commands
{
    public class ExcluirCondutorCommand : Command
    {
        public Guid Id { get; set; }

        public ExcluirCondutorCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirCondutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirCondutorValidation : AbstractValidator<ExcluirCondutorCommand>
        {
            public ExcluirCondutorValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CondutorCommandErrorMessages.IdNuloErroMsg);
            }
        }
    }
}
