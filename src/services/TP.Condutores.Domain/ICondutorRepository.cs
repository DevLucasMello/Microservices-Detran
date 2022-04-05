using System;
using System.Threading.Tasks;
using TP.Core.Data;
using TP.Core.DomainObjects;

namespace TP.Condutores.Domain
{
    public interface ICondutorRepository : IRepository<Condutor>
    {
        Task<PagedResult<Condutor>> ObterTodos(int pageSize, int pageIndex, string query);
        Task<PagedResult<Condutor>> ObterCondutoresPorPlaca(int pageSize, int pageIndex, string placa);
        Task<Condutor> ObterPorId(Guid id);
        Task<Condutor> ObterPorCPF(string cpf);
        Task<Veiculo> ObterVeiculoId(Guid veiculoId);
        void Adicionar(Condutor condutor);
        void Atualizar(Condutor condutor);
        void AtualizarCondutorVeiculo(Guid idCondutor, string idVeiculo, string placa);
        void Excluir(Condutor condutor);
        void RemoverVeiculoCondutor(Veiculo veiculo);
    }
}
