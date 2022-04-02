using System;
using System.Linq;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Messages;
using Xunit;

namespace TP.Veiculos.Application.Tests.Command
{
    public class ExcluirVeiculoCommandTests
    {
        [Fact(DisplayName = "Excluir Veículo Comando Válido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void ExcluirVeiculoCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var veiculoCommand = new ExcluirVeiculoCommand(Guid.NewGuid());

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Excluir Veículo Comando Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Commands")]
        public void ExcluirVeiculoCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculoCommand = new ExcluirVeiculoCommand(Guid.Empty);

            // Act
            var result = veiculoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(VeiculoCommandErrorMessages.IdNuloErroMsg, veiculoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
