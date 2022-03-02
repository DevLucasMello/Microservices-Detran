using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TP.Core.Mediator;
using TP.Veiculos.Application.Queries;
using TP.WebAPI.Core.Controllers;

namespace TP.Veiculos.API.Controllers
{
    [Authorize]
    public class VeiculoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IVeiculoQueries _veiculoQueries;

        public VeiculoController(IMediatorHandler mediator, IMapper mapper, IVeiculoQueries veiculoQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _veiculoQueries = veiculoQueries;
        }

        [HttpGet("veiculo")]
        public async Task<IActionResult> ObterTodosVeiculos()
        {
            var veiculos = await _veiculoQueries.ObterTodosVeiculos();

            return veiculos.ToList().Count <= 0 ? NotFound() : CustomResponse(veiculos);
        }

        [HttpGet("veiculo/documento/{cpf}")]
        public async Task<IActionResult> ObterVeiculosPorCPF(string cpf)
        {
            var veiculos = await _veiculoQueries.ObterVeiculosPorCPF(cpf);

            return veiculos.ToList().Count <= 0 ? NotFound() : CustomResponse(veiculos);
        }

        [HttpGet("veiculo/{id}")]
        public async Task<IActionResult> ObterCondutorPorId(Guid id)
        {
            var veiculo = await _veiculoQueries.ObterVeiculoPorId(id);

            return veiculo == null ? NotFound() : CustomResponse(veiculo);
        }

        [HttpGet("veiculo/placa/{placa}")]
        public async Task<IActionResult> ObterCondutorPorCpf(string placa)
        {
            var veiculo = await _veiculoQueries.ObterVeiculoPorPlaca(placa);

            return veiculo == null ? NotFound() : CustomResponse(veiculo);
        }
    }
}
