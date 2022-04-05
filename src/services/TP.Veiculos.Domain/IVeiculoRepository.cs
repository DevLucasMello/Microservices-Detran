using System;
using System.Threading.Tasks;
using TP.Core.Data;
using TP.Core.DomainObjects;

namespace TP.Veiculos.Domain
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<PagedResult<Veiculo>> ObterTodos(int pageSize, int pageIndex, string query);
        Task<PagedResult<Veiculo>> ObterVeiculosPorCPF(int pageSize, int pageIndex, string cpf);
        Task<Veiculo> ObterPorId(Guid id);
        void Adicionar(Veiculo veiculo, string idCondutor, string cpf);
        void Atualizar(Veiculo veiculo);
        void Excluir(Veiculo veiculo);
    }
}