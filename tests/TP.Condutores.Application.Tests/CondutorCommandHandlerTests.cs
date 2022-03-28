using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Tests.Fixtures.Tests;
using TP.Condutores.Domain;
using Xunit;

namespace TP.Condutores.Application.Tests
{
    [Collection(nameof(CondutorAutoMockerCollection))]
    public class CondutorCommandHandlerTests
    {
        private readonly CondutorTestsAutoMockerFixture _condutorTestsAutoMockerFixture;
        private CondutorCommandHandler _condutorHandler;

        public CondutorCommandHandlerTests(CondutorTestsAutoMockerFixture condutorTestsFixture)
        {            
            _condutorTestsAutoMockerFixture = condutorTestsFixture;
            _condutorHandler = _condutorTestsAutoMockerFixture.ObterCondutorHandler();
        }

        #region AdicionarCondutorCommand

        [Fact(DisplayName = "Adicionar Novo Condutor com Sucesso")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task AdicionarCondutor_NovoCondutor_DeveExecutarComSucesso()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();
            var condutorCommand = new AdicionarCondutorCommand(condutor.Nome, condutor.CPF, condutor.Telefone, condutor.Email, condutor.CNH, condutor.DataNascimento);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);            

            // Assert
            Assert.True(result.IsValid);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.Adicionar(It.IsAny<Condutor>()), Times.Once);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            //_mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Novo Condutor Command Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task AdicionarCondutor_CommandInvalido_DeveRetornarFalso()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorInvalido();
            var condutorCommand = new AdicionarCondutorCommand(condutor.Nome, condutor.CPF, condutor.Telefone, condutor.Email, condutor.CNH, condutor.DataNascimento);

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        #endregion        

        #region AtualizarCondutorCommand

        [Fact(DisplayName = "Atualizar Condutor Command Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task AtualizarCondutor_CommandInvalido_DeveRetornarFalso()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorInvalido();
            var condutorCommand = new AtualizarCondutorCommand(condutor.Id, condutor.Nome, condutor.CPF, condutor.Telefone, "teste@teste.com.br", condutor.CNH, condutor.DataNascimento);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Atualizar Condutor com Sucesso")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task Condutor_AtualizarCondutorCommand_DeveExecutarComSucesso()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.ObterPorId(condutor.Id)).
                Returns(Task.FromResult(condutor));

            var condutorCommand = new AtualizarCondutorCommand(condutor.Id, condutor.Nome, condutor.CPF, condutor.Telefone, "teste@teste.com.br", condutor.CNH, condutor.DataNascimento);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.Atualizar(It.IsAny<Condutor>()), Times.Once);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Obter Condutor com Por Id Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task CondutorIdInvalido_AtualizarCondutorCommand_NaoDevePassarNaValidacao()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.ObterPorId(Guid.NewGuid())).
                Returns(Task.FromResult(condutor));

            var condutorCommand = new AtualizarCondutorCommand(condutor.Id, condutor.Nome, condutor.CPF, condutor.Telefone, "teste@teste.com.br", condutor.CNH, condutor.DataNascimento);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.ObterPorId(condutor.Id), Times.Once);
            Assert.Contains("Condutor não encontrado.", result.Errors.Select(c => c.ErrorMessage));
        }

        #endregion

        #region AtualizarVeiculoCondutorCommand

        [Fact(DisplayName = "Atualizar Veiculo Condutor Command Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task AtualizarVeiculoCondutor_CommandInvalido_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _condutorTestsAutoMockerFixture.VeiculoInvalido();
            var veiculoCommand = new AtualizarVeiculoCondutorCommand(veiculo.CondutorId, Guid.Parse(veiculo.VeiculoId), veiculo.Placa);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Atualizar Veiculo Condutor com Sucesso")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task VeiculoCondutor_AtualizarVeiculoCondutorCommand_DeveExecutarComSucesso()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();
            var veiculo = _condutorTestsAutoMockerFixture.VeiculoValido();

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.ObterPorId(condutor.Id)).
                Returns(Task.FromResult(condutor));

            var veiculoCommand = new AtualizarVeiculoCondutorCommand(condutor.Id, Guid.Parse(veiculo.VeiculoId), veiculo.Placa);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.AtualizarCondutorVeiculo(condutor.Id, veiculo.VeiculoId, veiculo.Placa), Times.Once);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Obter Veiculo Condutor com Por Id Inválido")]
        [Trait("Categoria", "CondutoresAPI - Condutor Command Handler")]
        public async Task CondutorIdInvalido_AtualizarVeiculoCondutorCommand_NaoDevePassarNaValidacao()
        {
            // Arrange
            var condutor = _condutorTestsAutoMockerFixture.CondutorValido();

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.ObterPorId(Guid.NewGuid())).
                Returns(Task.FromResult(condutor));

            var condutorCommand = new AtualizarCondutorCommand(condutor.Id, condutor.Nome, condutor.CPF, condutor.Telefone, "teste@teste.com.br", condutor.CNH, condutor.DataNascimento);

            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _condutorHandler.Handle(condutorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _condutorTestsAutoMockerFixture._mocker.GetMock<ICondutorRepository>().Verify(r => r.ObterPorId(condutor.Id), Times.Once);
            Assert.Contains("Condutor não encontrado.", result.Errors.Select(c => c.ErrorMessage));
        }

        #endregion

        #region ExcluirCondutorCommand
        #endregion

        #region ExcluirVeiculoCondutorCommand
        #endregion
    }
}