using FluentValidation;
using System;
using TP.Condutores.Application.Messages;
using TP.Core.DomainObjects;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Condutores.Application.Commands
{
    public class AtualizarCondutorCommand : Command
    {
        public Guid Id { get; set; }
        public Nome Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CNH { get; set; }
        public DateTime? DataNascimento { get; set; }

        public AtualizarCondutorCommand(Guid id, Nome nome, string cpf, string telefone, string email, string cnh, DateTime? dataNascimento)
        {
            Id = id;
            AggregateId = id;
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            CNH = cnh;
            DataNascimento = dataNascimento;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarCondutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarCondutorValidation : AbstractValidator<AtualizarCondutorCommand>
        {
            public AtualizarCondutorValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CondutorCommandErrorMessages.IdNuloErroMsg);

                RuleFor(n => n.Nome.PrimeiroNome)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.PrimeiroNomeNuloErroMsg)
                    .Length(3, 150)
                    .WithMessage(CondutorCommandErrorMessages.PrimeiroNomeQtdErroMsg);

                RuleFor(n => n.Nome.UltimoNome)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.UltimoNomeNuloErroMsg)
                    .Length(3, 150)
                    .WithMessage(CondutorCommandErrorMessages.UltimoNomeQtdErroMsg);

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.CPFNuloErroMsg)
                    .Must(MethodsUtils.IsCpfValid)
                    .WithMessage(CondutorCommandErrorMessages.CPFInvalidoErroMsg);

                RuleFor(c => c.Telefone)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.TelefoneNuloErroMsg)
                    .MinimumLength(9)
                    .WithMessage(CondutorCommandErrorMessages.TelefoneQtdErroMsg);

                RuleFor(s => s.Email)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.EmailNuloErroMsg)
                    .EmailAddress()
                    .WithMessage(CondutorCommandErrorMessages.EmailInvalidoErroMsg);

                RuleFor(c => c.CNH)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.CNHNuloErroMsg);

                RuleFor(c => c.DataNascimento)
                    .NotEmpty()
                    .WithMessage(CondutorCommandErrorMessages.DataNascimentoNuloErroMsg)
                    .Must(MethodsUtils.CondutorMaiorDeIdade)
                    .WithMessage(CondutorCommandErrorMessages.DataNascimentoMenor18ErroMsg);
            }
        }
    }
}