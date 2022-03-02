using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var veiculos = await _context.Condutores
                                .AsNoTracking()
                                .Where(p => p.CPF == cpf)
                                .Select(p => p.VeiculoId)
                                .ToListAsync();

            return await _context.Veiculos
                .AsNoTracking()
                .Where(p => veiculos.Contains(p.Id))
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

        public async Task<CondutorVeiculo> ObterCondutorId(Guid condutorId)
        {
            return await _context.Condutores.AsNoTracking().FirstOrDefaultAsync(p => p.Id == condutorId);
        }

        public void Adicionar(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
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
