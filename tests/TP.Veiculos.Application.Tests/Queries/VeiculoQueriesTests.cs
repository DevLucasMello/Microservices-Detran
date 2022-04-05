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
        private readonly int pageSize;
        private readonly int pageIndex;
        private readonly string query;

        public VeiculoQueriesTests(VeiculoTestsAutoMockerFixture veiculoTestsAutoMockerFixture)
        {
            _veiculoTestsAutoMockerFixture = veiculoTestsAutoMockerFixture;
            _veiculoQueries = _veiculoTestsAutoMockerFixture.ObterVeiculoQueries();
            pageSize = 8;
            pageIndex = 1;
            query = null;
        }

        [Fact(DisplayName = "Obter Todos Veículos")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Queries")]
        public async void ObterTodosVeiculosQuery_QueryValida_DeveRetornarListaCondutores()
        {
            // Arrange
            var veiculos = _veiculoTestsAutoMockerFixture.ObterVeiculosPaginados();

            _veiculoTestsAutoMockerFixture._veiculoRepository.Setup(c => c.ObterTodos(pageSize, pageIndex, query))
                .Returns(Task.FromResult(veiculos));

            // Act
            var condutoresQuery = await _veiculoQueries.ObterTodosVeiculos(pageSize, pageIndex, query);

            // Assert
            _veiculoTestsAutoMockerFixture._veiculoRepository.Verify(r => r.ObterTodos(pageSize, pageIndex, query), Times.Once);
            Assert.True(condutoresQuery.List.Any());
        }
    }
}