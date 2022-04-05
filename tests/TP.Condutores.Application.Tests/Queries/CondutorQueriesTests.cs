using System.Linq;
using System.Threading.Tasks;
using Moq;
using TP.Condutores.Application.Queries;
using TP.Condutores.Application.Tests.Fixtures.Tests;
using Xunit;

namespace TP.Condutores.Application.Tests.Queries
{
    [Collection(nameof(CondutorAutoMockerCollection))]
    public class CondutorQueriesTests
    {
        private readonly CondutorTestsAutoMockerFixture _condutorTestsAutoMockerFixture;
        private readonly CondutorQueries _condutorQueries;
        private readonly int pageSize;
        private readonly int pageIndex;
        private readonly string query;

        public CondutorQueriesTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
            _condutorQueries = _condutorTestsAutoMockerFixture.ObterCondutorQueries();
            pageSize = 8;
            pageIndex = 1;
            query = null;
        }

        [Fact(DisplayName = "Obter Todos Condutores")]
        [Trait("Categoria", "CondutoresAPI - Condutor Queries")]
        public async void ObterTodosCondutoresQuery_QueryValida_DeveRetornarListaCondutores()
        {
            // Arrange
            var condutores = _condutorTestsAutoMockerFixture.ObterCondutoresPaginados();

            _condutorTestsAutoMockerFixture._condutorRepository.Setup(c => c.ObterTodos(pageSize, pageIndex, query))
                .Returns(Task.FromResult(condutores));

            // Act
            var condutoresQuery = await _condutorQueries.ObterTodosCondutores(pageSize, pageIndex, query);

            // Assert
            _condutorTestsAutoMockerFixture._condutorRepository.Verify(r => r.ObterTodos(pageSize, pageIndex, query), Times.Once);
            Assert.True(condutoresQuery.List.Any());
        }
    }
}