using System.Net.Http.Json;
using System.Threading.Tasks;
using TP.Core.Tests.Config;
using TP.Veiculos.API.Tests.Config;
using TP.Veiculos.Application.ViewModels;
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
        [Trait("Categoria", "VeiculosAPI - Integração")]
        public async Task VeiculosApi_AdicionarVeiculo_DeveRetornarComSucesso()
        {
            // Arrange
            var veiculo = new AdicionarVeiculoViewModel
            {
                CondutorId = "CC0EF744-4ABE-41D0-A9D0-91CF5ED96A99",
                Placa = "HZP8125",
                Modelo = "Fiesta",
                Marca = "Ford",
                Cor = "Cinza",
                AnoFabricacao = 1998,
                CPF = "53843823090"
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("veiculo", veiculo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Atualizar Novo Veiculo"), TestPriority(2)]
        [Trait("Categoria", "VeiculosAPI - Integração")]
        public async Task VeiculosApi_AtualizarVeiculo_DeveRetornarComSucesso()
        {
            // Arrange
            string id = "2F04990E-896D-4553-92A2-4F38AA97024A";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);
            
            var getResponse = await _testsFixture.Client.GetAsync($"veiculo/{id}");
            var veiculo = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirVeiculoViewModel>(getResponse);

            var veiculoAtualizar = new AtualizarVeiculoViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Cor = veiculo.Cor,
                AnoFabricacao = 2002
            };

            // Act
            var postResponse = await _testsFixture.Client.PutAsJsonAsync($"veiculo/{veiculoAtualizar.Id}", veiculo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Excluir Novo Veiculo"), TestPriority(1)]
        [Trait("Categoria", "VeiculosAPI - Integração")]
        public async Task VeiculosApi_ExcluirVeiculo_DeveRetornarComSucesso()
        {
            // Arrange
            string id = "2F04990E-896D-4553-92A2-4F38AA97024A";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var getResponse = await _testsFixture.Client.GetAsync($"veiculo/{id}");
            var veiculo = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirVeiculoViewModel>(getResponse);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"veiculo/{veiculo.Id}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}