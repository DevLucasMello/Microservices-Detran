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
        private CondutorQueries _condutorQueries;

        public CondutorQueriesTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
            _condutorQueries = _condutorTestsAutoMockerFixture.ObterCondutorQueries();
        }

        [Fact(DisplayName = "Obter Todos Condutores")]
        [Trait("Categoria", "CondutoresAPI - Condutor Queries")]
        public async void ObterTodosCondutoresQuery_QueryValida_DeveRetornarListaCondutores()
        {
            // Arrange
            var condutores = _condutorTestsAutoMockerFixture.ObterCondutores();

            _condutorTestsAutoMockerFixture._condutorRepository.Setup(c => c.ObterTodos())
                .Returns(Task.FromResult(condutores));

            // Act
            var condutoresQuery = await _condutorQueries.ObterTodosCondutores();

            // Assert
            _condutorTestsAutoMockerFixture._condutorRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(condutoresQuery.Any());
        }
    }
}