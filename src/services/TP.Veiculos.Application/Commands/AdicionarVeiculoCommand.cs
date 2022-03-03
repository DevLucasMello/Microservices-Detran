using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;

namespace TP.Veiculos.Application.Commands
{
    public class AdicionarVeiculoCommand : Command
    {        
        public Guid CondutorId { get; set; }
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Marca { get; private set; }
        public string Cor { get; private set; }
        public int AnoFabricacao { get; private set; }
        public string CPF { get; set; }

        public AdicionarVeiculoCommand(Guid condutorId, string placa, string modelo, string marca, string cor, int anoFabricacao, string cpf)
        {           
            CondutorId = condutorId;
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Cor = cor;
            AnoFabricacao = anoFabricacao;
            CPF = cpf;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarVeiculoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarVeiculoValidation : AbstractValidator<AdicionarVeiculoCommand>
        {
            public AdicionarVeiculoValidation()
            {
                RuleFor(c => c.CondutorId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do condutor inválido");

                RuleFor(v => v.Placa)
                    .NotEmpty()
                    .WithMessage("A Placa deve ser informada")
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage("A Placa informada é inválida");

                RuleFor(v => v.Modelo)
                    .NotEmpty()
                    .WithMessage("O Modelo deve ser informado");

                RuleFor(s => s.Marca)
                    .NotEmpty()
                    .WithMessage("O Email não foi informado");

                RuleFor(c => c.Cor)
                    .NotEmpty()
                    .WithMessage("A CNH deve ser informada");

                RuleFor(c => c.AnoFabricacao)
                    .NotEmpty()
                    .WithMessage("O Ano de Fabricação deve ser informado");

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage("O CPF deve ser informado")
                    .Must(MethodsUtils.IsCpfValid)
                    .WithMessage("O CPF informado é inválido");
            }
        }
    }
}