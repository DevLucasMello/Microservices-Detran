using System;
using System.Linq;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Messages;
using TP.Condutores.Application.Tests.Fixtures.Tests;
using Xunit;

namespace TP.Condutores.Application.Tests.Command
{
    [Collection(nameof(CondutorAutoMockerCollection))]
    public class ExcluirVeiculoCondutorCommandTests
    {
        private readonly CondutorTestsAutoMockerFixture _condutorTestsAutoMockerFixture;

        public ExcluirVeiculoCondutorCommandTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
        }

        [Fact(DisplayName = "Excluir Veículo Condutor Comando Válido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void ExcluirVeiculoCondutorCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _condutorTestsAutoMockerFixture.VeiculoValido();
            var condutorCommand = new ExcluirVeiculoCondutorCommand(Guid.Parse(veiculo.VeiculoId), veiculo.Placa);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Excluir Veículo Condutor Comando Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void ExcluirVeiculoCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _condutorTestsAutoMockerFixture.VeiculoInvalido();
            var condutorCommand = new ExcluirVeiculoCondutorCommand(Guid.Parse(veiculo.VeiculoId), veiculo.Placa);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(CondutorCommandErrorMessages.VeiculoIdNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.PlacaNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Excluir Veículo Condutor Comando Placa Inválida")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void ExcluirVeiculoCondutorCommand_ComandoPlacaInvalida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _condutorTestsAutoMockerFixture.VeiculoPlacaInvalida();
            var condutorCommand = new ExcluirVeiculoCondutorCommand(Guid.Parse(veiculo.VeiculoId), veiculo.Placa);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(CondutorCommandErrorMessages.PlacaInvalidaErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
