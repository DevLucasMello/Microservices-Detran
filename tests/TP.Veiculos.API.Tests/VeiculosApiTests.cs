using System.Threading.Tasks;
using TP.Veiculos.API.Tests.Config;
using Xunit;

namespace TP.Veiculos.API.Tests
{
    [TestCaseOrderer("TP.Veiculos.API.Tests.PriorityOrderer", "TP.Veiculos.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class VeiculosApiTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public VeiculosApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Novo Veiculo"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Veiculos")]
        public async Task VeiculosApi_AdicionarVeiculo_DeveRetornarComSucesso()
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

            //await _testsFixture.RealizarLoginApi();
            //_testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            //var postResponse = await _testsFixture.Client.PostAsJsonAsync("condutor", condutor);

            // Assert
            //postResponse.EnsureSuccessStatusCode();

            await Task.CompletedTask;
        }
    }
}