using System;
using System.Linq;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Messages;
using TP.Core.DomainObjects;
using Xunit;
namespace TP.Condutores.Application.Tests.Condutor
{
    public class AdicionarCondutorCommandTests
    {
        [Fact(DisplayName = "Adicionar Condutor Comando Válido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AdicionarCondutorCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var condutorCommand = new AdicionarCondutorCommand(new Nome("Lucas", "Santos"), "78289985037", "1111-2222", "teste@teste.com.br", "82954171198", new DateTime(1990, 02, 11));

            // Act
            var result = condutorCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Condutor Comando Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AdicionarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var condutorCommand = new AdicionarCondutorCommand(new Nome("", ""), "", "", "", "", null);

            // Act
            var result = condutorCommand.EhValido();
            var erros = condutorCommand.ValidationResult.Errors.Count;

            // Assert
            Assert.False(result);
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