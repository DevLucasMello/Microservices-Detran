using FluentValidation;
using System;
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
        public DateTime DataNascimento { get; set; }

        public AtualizarCondutorCommand(Guid id, Nome nome, string cpf, string telefone, string email, string cnh, DateTime dataNascimento)
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
                    .WithMessage("Id do condutor inválido");

                RuleFor(n => n.Nome.PrimeiroNome)
                    .NotEmpty()
                    .WithMessage("O primeiro nome não foi informado")
                    .Length(3, 150)
                    .WithMessage("O campo nome deve ter entre 3 e 150 caracteres");

                RuleFor(n => n.Nome.UltimoNome)
                    .NotEmpty()
                    .WithMessage("O último nome não foi informado")
                    .Length(3, 150)
                    .WithMessage("O campo nome deve ter entre 3 e 150 caracteres");

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage("O CPF deve ser informado")
                    .Must(MethodsUtils.IsCpfValid)
                    .WithMessage("O CPF informado é inválido");

                RuleFor(c => c.Telefone)
                    .NotEmpty()
                    .WithMessage("O Telefone deve ser preenchido")
                    .MinimumLength(9)
                    .WithMessage("Informe o telefone com no mínimo 9 digitos");

                RuleFor(s => s.Email)
                    .NotEmpty()
                    .WithMessage("O Email não foi informado")
                    .EmailAddress()
                    .WithMessage("Endereço de E-mail inválido");

                RuleFor(c => c.CNH)
                    .NotEmpty()
                    .WithMessage("A CNH deve ser informada");

                RuleFor(c => c.DataNascimento)
                    .NotEmpty()
                    .WithMessage("A Data de Nascimento deve ser informada")
                    .Must(MethodsUtils.CondutorMaiorDeIdade)
                    .WithMessage("O Condutor deve ter no mínimo 18 anos");
            }
        }
    }
}