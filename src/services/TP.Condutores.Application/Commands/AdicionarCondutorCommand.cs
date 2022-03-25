using FluentValidation;
using System;
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
        public static string PrimeiroNomeErroMsg => "O primeiro nome não foi informado";
        public static string UltimoNomeErroMsg => "O último nome não foi informado";
        public static string CPFErroMsg => "O CPF deve ser informado";
        public static string TelefoneErroMsg => "O Telefone deve ser preenchido";
        public static string EmailErroMsg => "O Email não foi informado";
        public static string CNHErroMsg => "A CNH deve ser informada";
        public static string DataNascimentoErroMsg => "A Data de Nascimento deve ser informada";
        public static string PrimeiroNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string UltimoNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string CPFInvalidoErroMsg => "O CPF informado é inválido";
        public static string TelefoneQtdErroMsg => "Informe o telefone com no mínimo 9 digitos";
        public static string EmailInvalidoErroMsg => "Endereço de E-mail inválido";
        public static string DataNascimentoMenor18ErroMsg => "O Condutor deve ter no mínimo 18 anos";
        public AdicionarCondutorValidation()
        {
            RuleFor(n => n.Nome.PrimeiroNome)
                .NotEmpty()
                .WithMessage(PrimeiroNomeErroMsg)
                .Length(3, 150)
                .WithMessage(PrimeiroNomeQtdErroMsg);

            RuleFor(n => n.Nome.UltimoNome)
                .NotEmpty()
                .WithMessage(UltimoNomeErroMsg)
                .Length(3, 150)
                .WithMessage(UltimoNomeQtdErroMsg);

            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage(CPFErroMsg)
                .Must(MethodsUtils.IsCpfValid)
                .WithMessage(CPFInvalidoErroMsg);

            RuleFor(c => c.Telefone)
                .NotEmpty()
                .WithMessage(TelefoneErroMsg)
                .MinimumLength(9)
                .WithMessage(TelefoneQtdErroMsg);

            RuleFor(s => s.Email)
                .NotEmpty()
                .WithMessage(EmailErroMsg)
                .EmailAddress()
                .WithMessage(EmailInvalidoErroMsg);

            RuleFor(c => c.CNH)
                .NotEmpty()
                .WithMessage(CNHErroMsg);

            RuleFor(c => c.DataNascimento)
                .NotEmpty()
                .WithMessage(DataNascimentoErroMsg)
                .Must(MethodsUtils.CondutorMaiorDeIdade)
                .WithMessage(DataNascimentoMenor18ErroMsg);
        }
    }
}