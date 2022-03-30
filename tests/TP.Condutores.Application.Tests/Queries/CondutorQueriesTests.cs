using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TP.Condutores.Application.Queries;
using TP.Condutores.Application.Tests.Fixtures.Tests;
using TP.Condutores.Application.ViewModels;
using TP.Condutores.Domain;
using Xunit;

namespace TP.Condutores.Application.Tests.Queries
{
    [Collection(nameof(CondutorAutoMockerCollection))]
    public class CondutorQueriesTests
    {
        private readonly CondutorTestsAutoMockerFixture _condutorTestsAutoMockerFixture;
        private CondutorQueries _condutorQueries;
        private IEnumerable<ExibirCondutorViewModel> _listaCondutores;
        private IMapper _mapper;

        public CondutorQueriesTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
            _condutorQueries = _condutorTestsAutoMockerFixture.ObterCondutorQueries();
            _listaCondutores = new List<ExibirCondutorViewModel>();
            _mapper = _condutorTestsAutoMockerFixture.ObterCondutorMapper();
        }

        [Fact(DisplayName = "Obter Todos Condutores")]
        [Trait("Categoria", "CondutoresAPI - Condutor Queries")]
        public async void ObterTodosCondutoresQuery_QueryValida_DeveRetornarListaCondutores()
        {
            // Arrange
            var condutores = _condutorTestsAutoMockerFixture.ObterCondutores();            

            _listaCondutores = _mapper.Map<IEnumerable<ExibirCondutorViewModel>>(condutores);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorQueries>().Setup(r => r.ObterTodosCondutores()).
                Returns(Task.FromResult(_listaCondutores));

            //_condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.ObterTodos()).
            //    Returns(Task.FromResult(condutores));

            // Act
            var condutoresQuery = await _condutorQueries.ObterTodosCondutores();

            // Assert
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(condutoresQuery.Any());
        }
    }
}