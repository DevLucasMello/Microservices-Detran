using FluentValidation;
using System;
using TP.Core.Messages;
using TP.Core.Utils;
using TP.Veiculos.Application.Messages;

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
                    .WithMessage(VeiculoCommandErrorMessages.CondutorIdNuloErroMsg);

                RuleFor(v => v.Placa)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.PlacaNuloErroMsg)
                    .Must(MethodsUtils.IsPlaqueValid)
                    .WithMessage(VeiculoCommandErrorMessages.PlacaInvalidaErroMsg);

                RuleFor(v => v.Modelo)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.ModeloNuloErroMsg);

                RuleFor(s => s.Marca)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.MarcaNuloErroMsg);

                RuleFor(c => c.Cor)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.CorNuloErroMsg);

                RuleFor(c => c.AnoFabricacao)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.AnoFabricacaoNuloErroMsg);

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage(VeiculoCommandErrorMessages.CPFNuloErroMsg)
                    .Must(MethodsUtils.IsCpfValid)
                    .WithMessage(VeiculoCommandErrorMessages.CPFInvalidoErroMsg);
            }
        }
    }
}