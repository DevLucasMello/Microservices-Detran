using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Condutores.Domain;
using TP.Core.Data;

namespace TP.Condutores.Infra.Data.Repository
{
    public class CondutorRepository : ICondutorRepository
    {
        private readonly CondutoresContext _context;

        public CondutorRepository(CondutoresContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Condutor>> ObterTodos()
        {
            return await _context.Condutores               
                .AsNoTracking()                
                .ToListAsync();
        }

        public async Task<IEnumerable<Condutor>> ObterCondutoresPorPlaca(string placa)
        {
            var condutores = await _context.Veiculos
                                .AsNoTracking()
                                .Where(p => p.Placa == placa)
                                .Select(p => p.CondutorId)
                                .ToListAsync();

            return await _context.Condutores                
                .AsNoTracking()
                .Where(p => condutores.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<Condutor> ObterPorId(Guid id)
        {
            return await _context.Condutores.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Condutor> ObterPorCPF(string cpf)
        {
            return await _context.Condutores.AsNoTracking().FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<VeiculoCondutor> ObterVeiculoId(Guid veiculoId)
        {
            return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == veiculoId);
        }

        public void Adicionar(Condutor condutor)
        {
            _context.Condutores.Add(condutor);
        }

        public void Atualizar(Condutor condutor)
        {
            _context.Condutores.Update(condutor);
        }

        public void Atualizar(Condutor condutor, Guid veiculoId, string placa)
        {
            var condutorVeiculo = condutor.AdicionarVeiculo(condutor, veiculoId, placa);

            _context.Add(condutorVeiculo);
        }

        public void Excluir(Condutor condutor)
        {
            _context.Condutores.Remove(condutor);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
