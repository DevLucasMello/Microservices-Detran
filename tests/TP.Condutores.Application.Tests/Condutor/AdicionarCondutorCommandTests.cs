using System;
using System.Linq;
using TP.Condutores.Application.Commands;
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
            Assert.Contains(AdicionarCondutorValidation.PrimeiroNomeErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.UltimoNomeErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.CPFErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.TelefoneErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.EmailErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.CNHErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.DataNascimentoErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.PrimeiroNomeQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.UltimoNomeQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.CPFInvalidoErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.TelefoneQtdErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.EmailInvalidoErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarCondutorValidation.DataNascimentoMenor18ErroMsg, condutorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}