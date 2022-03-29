using System.Net.Http.Json;
using System.Threading.Tasks;
using TP.Condutores.API.Tests.Config;
using TP.Condutores.Application.ViewModels;
using Xunit;

namespace TP.Condutores.API.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CondutoresApiTests
    {
        private readonly IntegrationTestsFixture _testsFixture;

        public CondutoresApiTests(IntegrationTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Novo Condutor"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Condutores")]
        public async Task CondutoresApi_AdicionarCondutor_DeveRetornarComSucesso()
        {
            // Arrange
            var condutor = new AdicionarCondutorViewModel
            {
                PrimeiroNome = "Teste",
                UltimoNome = "Teste Sobrenome",
                CPF = "53843823090",
                Telefone = "1111-2222",
                Email = "teste@teste.com.br",
                CNH = "11412046851",
                DataNascimento = "02/02/1990"
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("condutor", condutor);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
