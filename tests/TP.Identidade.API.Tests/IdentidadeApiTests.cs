using System.Threading.Tasks;
using TP.Identidade.API.Tests.Config;
using Xunit;

namespace TP.Identidade.API.Tests
{
    [TestCaseOrderer("TP.Identidade.API.Tests.PriorityOrderer", "TP.Identidade.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class IdentidadeApiTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public IdentidadeApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastrar Novo Usuário"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Identidade")]
        public async Task IdentidadeApi_CadastrarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            //var condutor = new AdicionarCondutorViewModel
            //{
            //    PrimeiroNome = "Teste",
            //    UltimoNome = "Teste Sobrenome",
            //    CPF = "53843823090",
            //    Telefone = "1111-2222",
            //    Email = "teste@teste.com.br",
            //    CNH = "11412046851",
            //    DataNascimento = "02/02/1990"
            //};

            // Act
            //var postResponse = await _testsFixture.Client.PostAsJsonAsync("condutor", condutor);

            // Assert
            //postResponse.EnsureSuccessStatusCode();

            await Task.CompletedTask;
        }

        [Fact(DisplayName = "Autenticar Usuário"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Identidade")]
        public async Task IdentidadeApi_AutenticarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            //var condutor = new AdicionarCondutorViewModel
            //{
            //    PrimeiroNome = "Teste",
            //    UltimoNome = "Teste Sobrenome",
            //    CPF = "53843823090",
            //    Telefone = "1111-2222",
            //    Email = "teste@teste.com.br",
            //    CNH = "11412046851",
            //    DataNascimento = "02/02/1990"
            //};

            // Act
            //var postResponse = await _testsFixture.Client.PostAsJsonAsync("condutor", condutor);

            // Assert
            //postResponse.EnsureSuccessStatusCode();

            await Task.CompletedTask;
        }
    }
}