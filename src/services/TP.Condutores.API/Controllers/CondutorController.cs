using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TP.Condutores.Application.Commands;
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

        public CondutorController(IMediatorHandler mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
