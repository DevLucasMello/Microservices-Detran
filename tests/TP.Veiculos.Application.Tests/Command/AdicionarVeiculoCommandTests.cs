using System;
using System.Linq;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Messages;
using TP.Veiculos.Application.Tests.Fixtures;
using Xunit;

namespace TP.Veiculos.Application.Tests.Command
{
    [Collection(nameof(VeiculoAutoMockerCollection))]
    public class AdicionarVeiculoCommandTests
    {
        private readonly VeiculoTestsAutoMockerFixture _veiculoTestsAutoMockerFixture;

        public AdicionarVeiculoCommandTests(VeiculoTestsAutoMockerFixture veiculoTestsAutoMockerFixture)
        {
            _veiculoTestsAutoMockerFixture = veiculoTestsAutoMockerFixture;
        }

        [Fact(DisplayName = "Adicionar Veículo Comando Válido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void AdicionarVeiculoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoComCondutor();

            var veiculoCommand = new AdicionarVeiculoCommand(
                Guid.Parse(_veiculoTestsAutoMockerFixture._idCondutor), veiculo.Placa,
                veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao,
                _veiculoTestsAutoMockerFixture._cpf);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Veículo Comando Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void AdicionarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoInvalido();

            var veiculoCommand = new AdicionarVeiculoCommand(
                Guid.Parse(_veiculoTestsAutoMockerFixture._idCondutor), veiculo.Placa,
                veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao,
                _veiculoTestsAutoMockerFixture._cpf);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(VeiculoCommandErrorMessages.CondutorIdNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.PlacaNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.ModeloNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.MarcaNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.CorNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.AnoFabricacaoNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.CPFNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.CPFInvalidoErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Veículo Comando Placa Inválida")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void AtualizarVeiculoCondutorCommand_ComandoPlacaInvalida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoPlacaInvalida();
            var veiculoCommand = new AdicionarVeiculoCommand(
                Guid.Parse(_veiculoTestsAutoMockerFixture._idCondutor), veiculo.Placa,
                veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao,
                _veiculoTestsAutoMockerFixture._cpf);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(VeiculoCommandErrorMessages.PlacaInvalidaErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
