using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Core.Data;

namespace TP.Veiculos.Domain
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<IEnumerable<Veiculo>> ObterTodos();
        Task<IEnumerable<Veiculo>> ObterVeiculosPorCPF(string cpf);
        Task<Veiculo> ObterPorId(Guid id);
        void Adicionar(Veiculo veiculo, string idCondutor, string cpf);
        void Atualizar(Veiculo veiculo);
        void Excluir(Veiculo veiculo);
    }
}