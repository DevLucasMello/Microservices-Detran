using System.Linq;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Messages;
using TP.Veiculos.Application.Tests.Fixtures;
using Xunit;

namespace TP.Veiculos.Application.Tests.Command
{
    [Collection(nameof(VeiculoAutoMockerCollection))]
    public class AtualizarVeiculoCommandTests
    {
        private readonly VeiculoTestsAutoMockerFixture _veiculoTestsAutoMockerFixture;

        public AtualizarVeiculoCommandTests(VeiculoTestsAutoMockerFixture veiculoTestsAutoMockerFixture)
        {
            _veiculoTestsAutoMockerFixture = veiculoTestsAutoMockerFixture;
        }

        [Fact(DisplayName = "Atualizar Veículo Comando Válido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void AtualizarVeiculoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoComCondutor();

            var veiculoCommand = new AtualizarVeiculoCommand(
                veiculo.Id, veiculo.Placa,
                veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Veículo Comando Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void AtualizarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoInvalido();

            var veiculoCommand = new AtualizarVeiculoCommand(
                veiculo.Id, veiculo.Placa,
                veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(VeiculoCommandErrorMessages.IdNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.PlacaNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.ModeloNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.MarcaNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.CorNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(VeiculoCommandErrorMessages.AnoFabricacaoNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
