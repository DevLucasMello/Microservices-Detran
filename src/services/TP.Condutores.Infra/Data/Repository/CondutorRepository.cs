using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TP.Condutores.Domain;
using TP.Core.Data;
using TP.Core.DomainObjects;

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

        public async Task<PagedResult<Condutor>> ObterTodos(int pageSize, int pageIndex, string query)
        {
            var sql = @$"SELECT c.Id,c.CPF, c.Telefone, c.Email, c.CNH, c.DataNascimento, c.PrimeiroNome, c.UltimoNome
                      FROM Condutor c
                      WHERE (@Nome IS NULL OR PrimeiroNome LIKE '%' + @Nome + '%') 
                      ORDER BY [PrimeiroNome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Condutor 
                      WHERE (@Nome IS NULL OR PrimeiroNome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });
            
            var condutores = multi.Read<Condutor, Nome, Condutor>((c, n) => {
                c.MapearNome(n.PrimeiroNome, n.UltimoNome);
                return c;
            }, "PrimeiroNome");

            var total = multi.Read<int>().FirstOrDefault();            

            return new PagedResult<Condutor>()
            {
                List = condutores,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<PagedResult<Condutor>> ObterCondutoresPorPlaca(int pageSize, int pageIndex, string placa)
        {
            var sql = @$"SELECT c.Id,c.CPF, c.Telefone, c.Email, c.CNH, c.DataNascimento, c.PrimeiroNome, c.UltimoNome
                      FROM Condutor c
                      WHERE (@Nome IS NULL OR c.Id IN (SELECT v.CondutorId FROM Veiculo v WHERE (v.Placa LIKE '%' + @Nome + '%')))                      
                      ORDER BY [PrimeiroNome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Condutor c
                      WHERE (@Nome IS NULL OR c.Id IN (SELECT v.CondutorId FROM Veiculo v WHERE (v.Placa LIKE '%' + @Nome + '%')))";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = placa });

            var condutores = multi.Read<Condutor, Nome, Condutor>((c, n) => {
                c.MapearNome(n.PrimeiroNome, n.UltimoNome);
                return c;
            }, "PrimeiroNome");

            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Condutor>()
            {
                List = condutores,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = placa
            };            
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
