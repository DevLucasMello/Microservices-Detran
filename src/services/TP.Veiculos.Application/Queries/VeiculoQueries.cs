using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Core.DomainObjects;
using TP.Veiculos.Application.ViewModels;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Application.Queries
{
    public interface IVeiculoQueries
    {
        Task<PagedResult<ExibirVeiculoViewModel>> ObterTodosVeiculos(int pageSize, int pageIndex, string query);
        Task<PagedResult<ExibirVeiculoViewModel>> ObterVeiculosPorCPF(int pageSize, int pageIndex, string cpf);
        Task<ExibirVeiculoViewModel> ObterVeiculoPorId(Guid id);
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

        public async Task<PagedResult<ExibirVeiculoViewModel>> ObterTodosVeiculos(int pageSize, int pageIndex, string query)
        {
            var veiculos = await _veiculoRepository.ObterTodos(pageSize, pageIndex, query);

            if (veiculos == null)
                return null;           

            var result = new PagedResult<ExibirVeiculoViewModel>()
            {
                List = _mapper.Map<IEnumerable<ExibirVeiculoViewModel>>(veiculos.List),
                TotalResults = veiculos.TotalResults,
                PageIndex = veiculos.PageIndex,
                PageSize = veiculos.PageSize,
                Query = veiculos.Query
            };

            return result;
        }

        public async Task<PagedResult<ExibirVeiculoViewModel>> ObterVeiculosPorCPF(int pageSize, int pageIndex, string cpf)
        {
            var veiculos = await _veiculoRepository.ObterVeiculosPorCPF(pageSize, pageIndex, cpf);

            if (veiculos == null)
                return null;

            var result = new PagedResult<ExibirVeiculoViewModel>()
            {
                List = _mapper.Map<IEnumerable<ExibirVeiculoViewModel>>(veiculos.List),
                TotalResults = veiculos.TotalResults,
                PageIndex = veiculos.PageIndex,
                PageSize = veiculos.PageSize,
                Query = veiculos.Query
            };

            return result;
        }

        public async Task<ExibirVeiculoViewModel> ObterVeiculoPorId(Guid id)
        {
            var veiculo = await _veiculoRepository.ObterPorId(id);

            if (veiculo == null)
                return null;

            return _mapper.Map<ExibirVeiculoViewModel>(veiculo);
        }
    }
}
