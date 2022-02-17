using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TP.Condutores.Application.Commands;
using TP.Core.Mediator;
using TP.WebAPI.Core.Controllers;

namespace TP.Condutores.API.Controllers
{
    [Authorize]
    public class CondutorController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public CondutorController(IMediatorHandler mediator)
        {
            _mediator = mediator;            
        }

        [HttpPost("condutor")]
        public async Task<IActionResult> AdicionarPedido(AdicionarCondutorCommand pedido)
        {            
            return CustomResponse(await _mediator.EnviarComando(pedido));
        }
    }
}
