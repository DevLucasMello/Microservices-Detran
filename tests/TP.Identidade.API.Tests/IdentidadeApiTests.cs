using System.Net.Http.Json;
using System.Threading.Tasks;
using TP.Identidade.API.Models;
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
        [Trait("Categoria", "IdentidadeAPI - Integração")]
        public async Task IdentidadeApi_CadastrarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            var usuario = new UsuarioRegistro
            {
                Email = "teste2@teste.com.br",
                Senha = "Teste@123",
                SenhaConfirmacao = "Teste@123"
            };

            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("nova-conta", usuario);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Autenticar Usuário"), TestPriority(2)]
        [Trait("Categoria", "IdentidadeAPI - Integração")]
        public async Task IdentidadeApi_AutenticarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            var usuario = new UsuarioLogin
            {
                Email = "teste2@teste.com.br",
                Senha = "Teste@123"
            };

            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("autenticar", usuario);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}