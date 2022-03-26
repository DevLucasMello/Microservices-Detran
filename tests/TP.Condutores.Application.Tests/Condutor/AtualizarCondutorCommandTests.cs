using System;
using TP.Core.DomainObjects;
using Xunit;

namespace TP.Condutores.Application.Tests.Condutor
{
    public class AtualizarCondutorCommandTests
    {
        [Fact(DisplayName = "Atualizar Condutor Comando Válido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AtualizarCondutorCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var condutorId = Guid.NewGuid();
            var condutorCommand = new Domain.Condutor(new Nome("Lucas", "Santos"), "78289985037", "1111-2222", "teste@teste.com.br", "82954171198", new DateTime(1990, 02, 11)).Id = condutorId;

            // Act

            // Assert
        }

        [Fact(DisplayName = "Atualizar Condutor Comando Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Commands")]
        public void AdicionarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange            

            // Act

            // Assertloc
        }
    }
}
