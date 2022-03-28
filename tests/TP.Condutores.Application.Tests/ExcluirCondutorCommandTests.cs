using System;
using System.Linq;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Messages;
using Xunit;

namespace TP.Condutores.Application.Tests
{
    public class ExcluirCondutorCommandTests
    {
        [Fact(DisplayName = "Excluir Condutor Comando Válido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void ExcluirCondutorCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var condutorCommand = new ExcluirCondutorCommand(Guid.NewGuid());

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Excluir Condutor Comando Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void ExcluirCondutorCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var condutorCommand = new ExcluirCondutorCommand(Guid.Empty);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(CondutorCommandErrorMessages.IdNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
