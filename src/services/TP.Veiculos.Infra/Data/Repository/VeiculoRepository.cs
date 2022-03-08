using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Core.Data;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Infra.Data.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly VeiculosContext _context;

        public VeiculoRepository(VeiculosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Veiculo>> ObterTodos()
        {
            return await _context.Veiculos
                .Include(c => c.Condutor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorCPF(string cpf)
        {
            return await _context.Veiculos
                                .FromSqlRaw(@"SELECT v.Id, v.Placa, v.Marca, v.Modelo, v.Cor, v.AnoFabricacao
                                              FROM Veiculo v
                                              WHERE v.Id IN (SELECT c.VeiculoId FROM Condutor c WHERE c.CPF = {0})", cpf)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Veiculo> ObterPorId(Guid id)
        {
            return await _context.Veiculos.Include(c => c.Condutor).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Adicionar(Veiculo veiculo, string idCondutor, string cpf)
        {
            
            var veiculoCondutor = veiculo.AdicionarCondutor(veiculo, idCondutor, cpf);

            _context.Add(veiculoCondutor);
        }

        public void Atualizar(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
        }

        public void Excluir(Veiculo veiculo)
        {
            _context.Veiculos.Remove(veiculo);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
