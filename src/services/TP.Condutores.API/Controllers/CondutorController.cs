using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Queries;
using TP.Condutores.Application.ViewModels;
using TP.Core.Mediator;
using TP.WebAPI.Core.Controllers;

namespace TP.Condutores.API.Controllers
{
    [Authorize]
    public class CondutorController : MainController
    {
        private readonly IMediatorHandler _mediator;        
        private readonly IMapper _mapper;
        private readonly ICondutorQueries _condutorQueries;

        public CondutorController(IMediatorHandler mediator, IMapper mapper, ICondutorQueries condutorQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _condutorQueries = condutorQueries;
        }

        [HttpGet("condutor")]
        public async Task<IActionResult> ObterTodosCondutores()
        {
            var condutores = await _condutorQueries.ObterTodosCondutores();

            return condutores.ToList().Count <= 0 ? NotFound() : CustomResponse(condutores);
        }
        
        [HttpGet("condutor/placa/{placa}")]
        public async Task<IActionResult> ObterCondutoresPorPlaca(string placa)
        {
            var condutores = await _condutorQueries.ObterCondutoresPorPlaca(placa);

            return condutores.ToList().Count <= 0 ? NotFound() : CustomResponse(condutores);
        }

        [HttpGet("condutor/{id}")]
        public async Task<IActionResult> ObterCondutorPorId(Guid id)
        {
            var condutor = await _condutorQueries.ObterCondutorPorId(id);

            return condutor == null ? NotFound() : CustomResponse(condutor);
        }

        [HttpGet("condutor/documento/{cpf}")]
        public async Task<IActionResult> ObterCondutorPorCpf(string cpf)
        {
            var condutor = await _condutorQueries.ObterCondutorPorCpf(cpf);

            return condutor == null ? NotFound() : CustomResponse(condutor);
        }

        [HttpPost("condutor")]
        public async Task<IActionResult> AdicionarCondutor(AdicionarCondutorViewModel condutorViewModel)
        {
            var condutor = _mapper.Map<AdicionarCondutorCommand>(condutorViewModel);
            return CustomResponse(await _mediator.EnviarComando(condutor));
        }

        [HttpPut("condutor/{id}")]
        public async Task<IActionResult> AtualizarCondutor(Guid id, AtualizarCondutorViewModel condutorViewModel)
        {
            var condutor = _mapper.Map<AtualizarCondutorCommand>(condutorViewModel);
            return CustomResponse(await _mediator.EnviarComando(condutor));
        }

        [HttpDelete("condutor/{id}")]
        public async Task<IActionResult> ExcluirCondutor(Guid id)
        {            
            return CustomResponse(await _mediator.EnviarComando(new ExcluirCondutorCommand(id)));
        }
    }
}
