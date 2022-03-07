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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorCPF(string cpf)
        {
            return await _context.Veiculos
                                .FromSqlRaw("SELECT V FROM Veiculo V INNER JOIN Condutor c ON C.CPF = {0}", cpf)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Veiculo> ObterPorId(Guid id)
        {
            return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Veiculo> ObterPorPlaca(string placa)
        {
            return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(c => c.Placa == placa);
        }

        public async Task<Condutor> ObterCondutorId(Guid condutorId)
        {
            return await _context.Condutores.AsNoTracking().FirstOrDefaultAsync(p => p.Id == condutorId);
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
