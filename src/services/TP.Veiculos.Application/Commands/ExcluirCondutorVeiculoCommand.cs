using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Veiculos.Application.Commands
{
    public class ExcluirCondutorVeiculoCommand : Command
    {
        public Guid VeiculoId { get; private set; }
        public Guid CondutorId { get; set; }
        public string CPF { get; private set; }

        public ExcluirCondutorVeiculoCommand(Guid veiculoId, Guid condutorId, string cpf)
        {
            VeiculoId = veiculoId;
            AggregateId = veiculoId;
            CondutorId = condutorId;
            CPF = cpf;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirCondutorVeiculoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirCondutorVeiculoCommandValidation : AbstractValidator<ExcluirCondutorVeiculoCommand>
        {
            public ExcluirCondutorVeiculoCommandValidation()
            {
                RuleFor(c => c.VeiculoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do veículo inválido");

                RuleFor(c => c.CondutorId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do condutor inválido");

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage("O CPF deve ser informado")
                    .Must(MethodsUtils.IsCpfValid)
                    .WithMessage("O CPF informado é inválido");
            }
        }
    }
}
