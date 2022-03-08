using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Include(c => c.Veiculo)
                .AsNoTracking()                
                .ToListAsync();
        }

        public async Task<IEnumerable<Condutor>> ObterCondutoresPorPlaca(string placa)
        {
            return await _context.Condutores
                                .FromSqlRaw(@"SELECT c.Id, c.PrimeiroNome, c.UltimoNome, c.CPF, c.CNH, c.Telefone, c.Email, c.DataNascimento
                                              FROM Condutor c
                                              WHERE c.Id IN (SELECT v.CondutorId FROM Veiculo v WHERE v.Placa = {0})", placa)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Condutor> ObterPorId(Guid id)
        {
            return await _context.Condutores.Include(c => c.Veiculo).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Condutor> ObterPorCPF(string cpf)
        {
            return await _context.Condutores.Include(c => c.Veiculo).AsNoTracking().FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<Veiculo> ObterVeiculoId(Guid veiculoId)
        {            
            return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(p => p.VeiculoId.ToLower() == veiculoId.ToString().ToLower());
        }

        public void Adicionar(Condutor condutor)
        {
            _context.Condutores.Add(condutor);
        }

        public void Atualizar(Condutor condutor)
        {
            _context.Condutores.Update(condutor);
        }

        public void AtualizarCondutorVeiculo(Guid idCondutor, string idVeiculo, string placa)
        {            
            _context.Veiculos.Add(new Veiculo(idCondutor, idVeiculo, placa));
        }

        public void Excluir(Condutor condutor)
        {
            _context.Condutores.Remove(condutor);
        }

        public void RemoverVeiculoCondutor(Veiculo veiculo)
        {
            _context.Veiculos.Remove(veiculo);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
