using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Messages;
using TP.Veiculos.Application.Tests.Fixtures;
using TP.Veiculos.Domain;
using Xunit;

namespace TP.Veiculos.Application.Tests.Command
{
    [Collection(nameof(VeiculoAutoMockerCollection))]
    public class VeiculoCommandHandlerTests
    {
        private readonly VeiculoTestsAutoMockerFixture _veiculoTestsAutoMockerFixture;
        private VeiculoCommandHandler _veiculoHandler;
        private IEnumerable<Veiculo> _veiculos;
        private IEnumerable<Condutor> _condutores;
        private readonly int pageSize;
        private readonly int pageIndex;

        public VeiculoCommandHandlerTests(VeiculoTestsAutoMockerFixture veiculoTestsFixture)
        {
            _veiculoTestsAutoMockerFixture = veiculoTestsFixture;
            _veiculoHandler = _veiculoTestsAutoMockerFixture.ObterVeiculoHandler();
            _veiculos = _veiculoTestsAutoMockerFixture.ObterVeiculos();
            _condutores = _veiculoTestsAutoMockerFixture.ObterCondutores();
            pageSize = 8;
            pageIndex = 1;
        }

        #region AdicionarVeiculoCommand

        [Fact(DisplayName = "Adicionar Novo Veículo com Sucesso")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AdicionarVeiculoCommand_NovoCondutor_DeveExecutarComSucesso()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();
            var condutor = _condutores.FirstOrDefault();
            var veiculoCommand = new AdicionarVeiculoCommand(Guid.Parse(condutor.CondutorId), veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao, condutor.CPF);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.Adicionar(It.IsAny<Veiculo>(), condutor.CondutorId, condutor.CPF), Times.Once);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Novo Veículo com Veículo Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AdicionarVeiculoCommand_VeiculoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoInvalido();
            var condutor = _condutores.FirstOrDefault();
            var veiculoCommand = new AdicionarVeiculoCommand(Guid.Parse(condutor.CondutorId), veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao, condutor.CPF);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar Novo Veículo com Condutor Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AdicionarVeiculoCommand_CondutorInvalido_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();
            var condutor = _veiculoTestsAutoMockerFixture.CondutorInvalido();
            var veiculoCommand = new AdicionarVeiculoCommand(Guid.Parse(condutor.CondutorId), veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao, condutor.CPF);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar Novo Veículo com Placa já Cadastrada")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AdicionarVeiculoCommand_PlacaJaCadastrada_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();
            var condutor = _veiculoTestsAutoMockerFixture.CondutorValido();
            var veiculosPaginados = _veiculoTestsAutoMockerFixture.ObterVeiculosPaginados(_veiculos);
            var veiculoCommand = new AdicionarVeiculoCommand(Guid.Parse(condutor.CondutorId), veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao, condutor.CPF);

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.ObterVeiculosPorCPF(50,1,condutor.CPF)).
                Returns(Task.FromResult(veiculosPaginados));

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(VeiculoCommandErrorMessages.PlacaCadastradaErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }

        #endregion

        #region AtualizarVeiculoCommand        

        [Fact(DisplayName = "Atualizar Veículo com Sucesso")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AtualizarVeiculoCommand_CommandoInvalido_DeveExecutarComSucesso()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.ObterPorId(veiculo.Id)).
                Returns(Task.FromResult(veiculo));

            var veiculoCommand = new AtualizarVeiculoCommand(veiculo.Id, veiculo.Placa, veiculo.Modelo, veiculo.Marca, "Branco", veiculo.AnoFabricacao);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.Atualizar(It.IsAny<Veiculo>()), Times.Once);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Veículo Command Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AtualizarVeiculoCommand_CommandoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoInvalido();
            var veiculoCommand = new AtualizarVeiculoCommand(veiculo.Id, veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Atualizar Veículo Obter Veículo com Id Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task AtualizarVeiculoCommand_IdInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.ObterPorId(Guid.NewGuid())).
                Returns(Task.FromResult(veiculo));

            var veiculoCommand = new AtualizarVeiculoCommand(veiculo.Id, veiculo.Placa, veiculo.Modelo, veiculo.Marca, veiculo.Cor, veiculo.AnoFabricacao);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(VeiculoCommandErrorMessages.VeiculoNaoEncontradoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.ObterPorId(veiculo.Id), Times.Once);
        }

        #endregion

        #region ExcluirVeiculoCommand        

        [Fact(DisplayName = "Excluir Veículo com Sucesso")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task ExcluirVeiculoCommand_CommandoInvalido_DeveExecutarComSucesso()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.ObterPorId(veiculo.Id)).
                Returns(Task.FromResult(veiculo));

            var veiculoCommand = new ExcluirVeiculoCommand(veiculo.Id);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.Excluir(It.IsAny<Veiculo>()), Times.Once);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Excluir Veículo Command Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task ExcluirVeiculoCommand_CommandoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var veiculo = _veiculoTestsAutoMockerFixture.VeiculoInvalido();
            var veiculoCommand = new ExcluirVeiculoCommand(Guid.Empty);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Excluir Veículo Obter Veículo com Id Inválido")]
        [Trait("Categoria", "VeiculosAPI - Veiculo Command Handler")]
        public async Task ExcluirVeiculoCommand_IdInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var veiculo = _veiculos.FirstOrDefault();

            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.ObterPorId(Guid.NewGuid())).
                Returns(Task.FromResult(veiculo));

            var veiculoCommand = new ExcluirVeiculoCommand(veiculo.Id);
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _veiculoHandler.Handle(veiculoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(VeiculoCommandErrorMessages.VeiculoNaoEncontradoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            _veiculoTestsAutoMockerFixture._mocker.GetMock<IVeiculoRepository>().Verify(r => r.ObterPorId(veiculo.Id), Times.Once);
        }

        #endregion
    }
}