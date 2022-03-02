using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Core.Data;

namespace TP.Condutores.Domain
{
    public interface ICondutorRepository : IRepository<Condutor>
    {
        Task<IEnumerable<Condutor>> ObterTodos();
        Task<IEnumerable<Condutor>> ObterCondutoresPorPlaca(string placa);
        Task<Condutor> ObterPorId(Guid id);
        Task<Condutor> ObterPorCPF(string cpf);
        Task<VeiculoCondutor> ObterVeiculoId(Guid veiculoId);
        void Adicionar(Condutor condutor);
        void Atualizar(Condutor condutor);
        void Excluir(Condutor condutor); 
    }
}
