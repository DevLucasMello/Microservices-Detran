using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Core.Data;
using TP.Core.DomainObjects;
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

        public async Task<PagedResult<Veiculo>> ObterTodos(int pageSize, int pageIndex, string query)
        {
            var sql = @$"SELECT v.Id,v.Placa, v.Modelo, v.Marca, v.Cor, v.AnoFabricacao
                      FROM Veiculo v
                      WHERE (@Nome IS NULL OR Modelo LIKE '%' + @Nome + '%') 
                      ORDER BY [Modelo] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Veiculo 
                      WHERE (@Nome IS NULL OR Modelo LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var veiculos = multi.Read<Veiculo>();

            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Veiculo>()
            {
                List = veiculos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<PagedResult<Veiculo>> ObterVeiculosPorCPF(int pageSize, int pageIndex, string cpf)
        {
            var sql = @$"SELECT v.Id,v.Placa, v.Modelo, v.Marca, v.Cor, v.AnoFabricacao
                      FROM Veiculo v
                      WHERE (@Nome IS NULL OR v.Id IN (SELECT c.VeiculoId FROM Condutor c WHERE (c.CPF LIKE '%' + @Nome + '%')))                      
                      ORDER BY [Modelo] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Veiculo v
                      WHERE (@Nome IS NULL OR v.Id IN (SELECT c.VeiculoId FROM Condutor c WHERE (c.CPF LIKE '%' + @Nome + '%')))";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = cpf });

            var veiculos = multi.Read<Veiculo>();

            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Veiculo>()
            {
                List = veiculos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = cpf
            };
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
