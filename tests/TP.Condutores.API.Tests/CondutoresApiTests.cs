using System.Net.Http.Json;
using System.Threading.Tasks;
using TP.Condutores.API.Tests.Config;
using TP.Condutores.Application.ViewModels;
using TP.Core.Tests.Config;
using Xunit;

namespace TP.Condutores.API.Tests
{
    [TestCaseOrderer("TP.Condutores.API.Tests.PriorityOrderer", "TP.Condutores.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CondutoresApiTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        private readonly int pageSize;
        private readonly int pageIndex;
        private readonly string query;

        public CondutoresApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
            pageSize = 8;
            pageIndex = 1;
            query = null;
        }

        [Fact(DisplayName = "Adicionar Novo Condutor"), TestPriority(1)]
        [Trait("Categoria", "CondutoresAPI - Integração")]
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

        [Fact(DisplayName = "Obter Todos Condutores"), TestPriority(2)]
        [Trait("Categoria", "CondutoresAPI - Integração")]
        public async Task CondutoresApi_ObterTodosCondutores_DeveRetornarComSucesso()
        {
            // Arrange            
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync($"condutor?ps={pageSize}&page={pageIndex}&q={query}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter Condutores por Placa", Skip = "Deve Adicionar um Veículo antes deste método"), TestPriority(3)]
        [Trait("Categoria", "CondutoresAPI - Integração")]
        public async Task CondutoresApi_ObterCondutoresPorPlaca_DeveRetornarComSucesso()
        {
            // Arrange
            string placa = "HZP8125";
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync($"condutor/placa?ps={pageSize}&page={pageIndex}&placa={placa}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Atualizar Condutor"), TestPriority(4)]
        [Trait("Categoria", "CondutoresAPI - Integração")]
        public async Task CondutoresApi_AtualizarCondutor_DeveRetornarComSucesso()
        {
            // Arrange
            string cpf = "53843823090";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var getResponse = await _testsFixture.Client.GetAsync($"condutor/documento/{cpf}");
            var condutor = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirCondutorViewModel>(getResponse);

            var condutorAtualizadar = new AtualizarCondutorViewModel
            {
                Id = condutor.Id,
                PrimeiroNome = "Lucas",
                UltimoNome = "Santos",
                CPF = condutor.CPF,
                Telefone = condutor.Telefone,
                Email = condutor.Email,
                CNH = condutor.CNH,
                DataNascimento = condutor.DataNascimento
            };

            // Act
            var postResponse = await _testsFixture.Client.PutAsJsonAsync($"condutor/{condutorAtualizadar.Id}", condutor);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Excluir Condutor"), TestPriority(5)]
        [Trait("Categoria", "CondutoresAPI - Integração")]
        public async Task CondutoresApi_ExcluirCondutor_DeveRetornarComSucesso()
        {
            // Arrange
            string cpf = "53843823090";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var getResponse = await _testsFixture.Client.GetAsync($"condutor/documento/{cpf}");
            var condutor = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirCondutorViewModel>(getResponse);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"condutor/{condutor.Id}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}