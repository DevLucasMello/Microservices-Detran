using System;
using System.Linq;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Messages;
using TP.Condutores.Application.Tests.Fixtures.Tests;
using Xunit;

namespace TP.Condutores.Application.Tests.Command
{
    [Collection(nameof(CondutorAutoMockerCollection))]
    public class AtualizarCondutorCommandTests
    {
        private readonly CondutorTestsAutoMockerFixture _condutorTestsAutoMockerFixture;

        public AtualizarCondutorCommandTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
        }

        [Fact(DisplayName = "Atualizar Condutor Comando Válido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AtualizarCondutorCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();
            var condutorCommand = new AtualizarCondutorCommand(Guid.NewGuid(), condutor.Nome, condutor.CPF, condutor.Telefone, condutor.Email, condutor.CNH, condutor.DataNascimento);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Condutor Comando Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AtualizarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorInvalido();
            var condutorCommand = new AtualizarCondutorCommand(Guid.Empty, condutor.Nome, condutor.CPF, condutor.Telefone, condutor.Email, condutor.CNH, condutor.DataNascimento);

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(CondutorCommandErrorMessages.IdNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.PrimeiroNomeNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.UltimoNomeNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.CPFNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.TelefoneNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.EmailNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.CNHNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.DataNascimentoNuloErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.PrimeiroNomeQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.UltimoNomeQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.CPFInvalidoErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.TelefoneQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.EmailInvalidoErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CondutorCommandErrorMessages.DataNascimentoMenor18ErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
