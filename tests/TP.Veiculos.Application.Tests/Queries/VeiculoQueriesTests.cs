using System.Linq;
using System.Threading.Tasks;
using Moq;
using TP.Veiculos.Application.Queries;
using TP.Veiculos.Application.Tests.Fixtures;
using Xunit;

namespace TP.Veiculos.Application.Tests.Queries
{
    [Collection(nameof(VeiculoAutoMockerCollection))]
    public class VeiculoQueriesTests
    {
        private readonly VeiculoTestsAutoMockerFixture _veiculoTestsAutoMockerFixture;
        private VeiculoQueries _veiculoQueries;

        public VeiculoQueriesTests(VeiculoTestsAutoMockerFixture veiculoTestsAutoMockerFixture)
        {
            _veiculoTestsAutoMockerFixture = veiculoTestsAutoMockerFixture;
            _veiculoQueries = _veiculoTestsAutoMockerFixture.ObterVeiculoQueries();
        }

        [Fact(DisplayName = "Obter Todos Veículos")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Queries")]
        public async void ObterTodosVeiculosQuery_QueryValida_DeveRetornarListaCondutores()
        {
            // Arrange
            var veiculos = _veiculoTestsAutoMockerFixture.ObterVeiculos();

            _veiculoTestsAutoMockerFixture._veiculoRepository.Setup(c => c.ObterTodos())
                .Returns(Task.FromResult(veiculos));

            // Act
            var condutoresQuery = await _veiculoQueries.ObterTodosVeiculos();

            // Assert
            _veiculoTestsAutoMockerFixture._veiculoRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(condutoresQuery.Any());
        }
    }
}