using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AdicionarPedido(AdicionarCondutorViewModel condutorViewModel)
        {
            var condutor = _mapper.Map<AdicionarCondutorCommand>(condutorViewModel);
            return CustomResponse(await _mediator.EnviarComando(condutor));
        }
    }
}
