using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TP.Bff.Detran.Models.Veiculo;
using TP.Bff.Detran.Services;
using TP.WebAPI.Core.Controllers;

namespace TP.Bff.Detran.Controllers
{
    [Authorize]
    public class VeiculoController : MainController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly ICondutorService _condutorService;

        public VeiculoController(IVeiculoService veiculoService, ICondutorService condutorService)
        {
            _veiculoService = veiculoService;
            _condutorService = condutorService;
        }

        [HttpGet]
        [Route("detran/veiculo")]
        public async Task<IActionResult> ObterTodosVeiculos([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return CustomResponse(await _veiculoService.ObterTodosVeiculos(ps, page, q));
        }

        [HttpGet]
        [Route("detran/veiculo/documento")]
        public async Task<IActionResult> ObterVeiculosPorCPF([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string cpf = null)
        {
            return CustomResponse(await _veiculoService.ObterVeiculosPorCPF(ps, page, cpf));
        }

        [HttpGet]
        [Route("detran/veiculo/{id}")]
        public async Task<IActionResult> ObterVeiculoPorId(Guid id)
        {
            return CustomResponse(await _veiculoService.ObterVeiculoPorId(id));
        }

        [HttpPost]
        [Route("detran/veiculo")]
        public async Task<IActionResult> AdicionarVeiculo(AdicionarVeiculoDTO veiculo)
        {
            var condutorExistente = await _condutorService.ObterCondutorPorId(Guid.Parse(veiculo.CondutorId));
            if (condutorExistente is null)
            {
                AdicionarErroProcessamento("Condutor não encontrado!");
                return CustomResponse();
            }

            if (condutorExistente.CPF != veiculo.CPF)
            {
                AdicionarErroProcessamento("CPF informado não pertence ao condutor!");
                return CustomResponse();
            }

            return CustomResponse(await _veiculoService.AdicionarVeiculo(veiculo));
        }

        [HttpPut]
        [Route("detran/veiculo/{id}")]
        public async Task<IActionResult> AtualizarVeiculo(Guid id, AtualizarVeiculoDTO veiculo)
        {
            var veiculoExistente = await _veiculoService.ObterVeiculoPorId(id);
            if (veiculoExistente is null)
            {
                AdicionarErroProcessamento("Veículo não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _veiculoService.AtualizarVeiculo(id, veiculo));
        }

        [HttpDelete]
        [Route("detran/veiculo/{id}")]
        public async Task<IActionResult> ExcluirVeiculo(Guid id)
        {
            var veiculoExistente = await _veiculoService.ObterVeiculoPorId(id);
            if (veiculoExistente is null)
            {
                AdicionarErroProcessamento("Veículo não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _veiculoService.ExcluirVeiculo(id));
        }
    }
}