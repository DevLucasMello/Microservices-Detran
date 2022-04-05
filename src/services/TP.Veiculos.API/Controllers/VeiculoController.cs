using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TP.Core.Mediator;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Queries;
using TP.Veiculos.Application.ViewModels;
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
        public async Task<IActionResult> ObterTodosVeiculos([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var veiculos = await _veiculoQueries.ObterTodosVeiculos(ps, page, q);

            return !veiculos.List.Any() ? NotFound() : CustomResponse(veiculos);
        }

        [HttpGet("veiculo/documento")]
        public async Task<IActionResult> ObterVeiculosPorCPF([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string cpf = null)
        {
            var veiculos = await _veiculoQueries.ObterVeiculosPorCPF(ps, page, cpf);

            return !veiculos.List.Any() ? NotFound() : CustomResponse(veiculos);
        }

        [HttpGet("veiculo/{id}")]
        public async Task<IActionResult> ObterVeiculoPorId(Guid id)
        {
            var veiculo = await _veiculoQueries.ObterVeiculoPorId(id);

            return veiculo == null ? NotFound() : CustomResponse(veiculo);
        }

        [HttpPost("veiculo")]
        public async Task<IActionResult> AdicionarVeiculo(AdicionarVeiculoViewModel veiculoViewModel)
        {
            var veiculo = _mapper.Map<AdicionarVeiculoCommand>(veiculoViewModel);
            return CustomResponse(await _mediator.EnviarComando(veiculo));
        }

        [HttpPut("veiculo/{id}")]
        public async Task<IActionResult> AtualizarVeiculo(Guid id, AtualizarVeiculoViewModel veiculoViewModel)
        {
            if (id.ToString().ToLower() != veiculoViewModel.Id.ToLower()) return NotFound();
            var veiculo = _mapper.Map<AtualizarVeiculoCommand>(veiculoViewModel);
            return CustomResponse(await _mediator.EnviarComando(veiculo));
        }

        [HttpDelete("veiculo/{id}")]
        public async Task<IActionResult> ExcluirVeiculo(Guid id)
        {
            return CustomResponse(await _mediator.EnviarComando(new ExcluirVeiculoCommand(id)));
        }
    }
}
