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
