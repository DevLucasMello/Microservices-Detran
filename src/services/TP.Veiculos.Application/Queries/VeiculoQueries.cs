using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Veiculos.Application.ViewModels;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Application.Queries
{
    public interface IVeiculoQueries
    {
        Task<IEnumerable<ExibirVeiculoViewModel>> ObterTodosVeiculos();
        Task<IEnumerable<ExibirVeiculoViewModel>> ObterVeiculosPorCPF(string cpf);
        Task<ExibirVeiculoViewModel> ObterVeiculoPorId(Guid id);
        Task<ExibirVeiculoViewModel> ObterVeiculoPorPlaca(string placa);
    }

    public class VeiculoQueries : IVeiculoQueries
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMapper _mapper;

        public VeiculoQueries(IVeiculoRepository veiculoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExibirVeiculoViewModel>> ObterTodosVeiculos()
        {
            var veiculos = await _veiculoRepository.ObterTodos();

            if (veiculos == null)
                return null;

            return _mapper.Map<IEnumerable<ExibirVeiculoViewModel>>(veiculos);
        }

        public async Task<IEnumerable<ExibirVeiculoViewModel>> ObterVeiculosPorCPF(string cpf)
        {
            var veiculos = await _veiculoRepository.ObterVeiculosPorCPF(cpf);

            if (veiculos == null)
                return null;

            return _mapper.Map<IEnumerable<ExibirVeiculoViewModel>>(veiculos);
        }

        public async Task<ExibirVeiculoViewModel> ObterVeiculoPorId(Guid id)
        {
            var veiculo = await _veiculoRepository.ObterPorId(id);

            if (veiculo == null)
                return null;

            return _mapper.Map<ExibirVeiculoViewModel>(veiculo);
        }

        public async Task<ExibirVeiculoViewModel> ObterVeiculoPorPlaca(string placa)
        {
            var veiculo = await _veiculoRepository.ObterPorPlaca(placa);

            if (veiculo == null)
                return null;

            return _mapper.Map<ExibirVeiculoViewModel>(veiculo);
        }
    }
}
