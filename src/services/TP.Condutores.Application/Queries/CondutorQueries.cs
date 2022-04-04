using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Condutores.Application.ViewModels;
using TP.Condutores.Domain;
using TP.Core.DomainObjects;

namespace TP.Condutores.Application.Queries
{
    public interface ICondutorQueries
    {
        Task<PagedResult<ExibirCondutorViewModel>> ObterTodosCondutores(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<ExibirCondutorViewModel>> ObterCondutoresPorPlaca(string placa);
        Task<ExibirCondutorViewModel> ObterCondutorPorId(Guid id);
        Task<ExibirCondutorViewModel> ObterCondutorPorCpf(string cpf);
    }

    public class CondutorQueries : ICondutorQueries
    {
        private readonly ICondutorRepository _condutorRepository;
        private readonly IMapper _mapper;

        public CondutorQueries(ICondutorRepository condutorRepository, IMapper mapper)
        {
            _condutorRepository = condutorRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ExibirCondutorViewModel>> ObterTodosCondutores(int pageSize, int pageIndex, string query = null)
        {
            var condutores = await _condutorRepository.ObterTodos(pageSize, pageIndex, query);

            if (condutores == null)
                return null;

            var result = new PagedResult<ExibirCondutorViewModel>()
            {
                List = _mapper.Map<IEnumerable<ExibirCondutorViewModel>>(condutores.List),
                TotalResults = condutores.TotalResults,
                PageIndex = condutores.PageIndex,
                PageSize = condutores.PageSize,
                Query = condutores.Query
            };

            return result;
        }

        public async Task<IEnumerable<ExibirCondutorViewModel>> ObterCondutoresPorPlaca(string placa)
        {
            var condutores = await _condutorRepository.ObterCondutoresPorPlaca(placa);

            if (condutores == null)
                return null;

            return _mapper.Map<IEnumerable<ExibirCondutorViewModel>>(condutores);
        }

        public async Task<ExibirCondutorViewModel> ObterCondutorPorId(Guid id)
        {
            var condutor = await _condutorRepository.ObterPorId(id);

            if (condutor == null)
                return null;

            return _mapper.Map<ExibirCondutorViewModel>(condutor);
        }

        public async Task<ExibirCondutorViewModel> ObterCondutorPorCpf(string cpf)
        {
            var condutor = await _condutorRepository.ObterPorCPF(cpf);

            if (condutor == null)
                return null;

            return _mapper.Map<ExibirCondutorViewModel>(condutor);
        }
    }
}