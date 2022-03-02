using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TP.Core.Mediator;
using TP.WebAPI.Core.Controllers;

namespace TP.Veiculos.API.Controllers
{
    [Authorize]
    public class VeiculoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;

        public VeiculoController(IMediatorHandler mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
