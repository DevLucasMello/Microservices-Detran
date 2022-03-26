using FluentValidation;
using System;
using TP.Condutores.Application.Messages;
using TP.Core.DomainObjects;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Condutores.Application.Commands
{
    public class AdicionarCondutorCommand : Command
    {
        public Nome Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CNH { get; set; }
        public DateTime? DataNascimento { get; set; }

        public AdicionarCondutorCommand(Nome nome, string cpf, string telefone, string email, string cnh, DateTime? dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            CNH = cnh;
            DataNascimento = dataNascimento;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarCondutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarCondutorValidation : AbstractValidator<AdicionarCondutorCommand>
    {        
        public AdicionarCondutorValidation()
        {
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