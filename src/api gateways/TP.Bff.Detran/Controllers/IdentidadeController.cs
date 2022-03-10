using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TP.Bff.Detran.Models.Identidade;
using TP.Bff.Detran.Services;
using TP.WebAPI.Core.Controllers;

namespace TP.Bff.Detran.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IIdentidadeService _identidadeService;

        public IdentidadeController(IIdentidadeService identidadeService)
        {
            _identidadeService = identidadeService;
        }

        [HttpPost]
        [Route("detran/nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistroDTO usuarioRegistro)
        {
            return CustomResponse(await _identidadeService.Registrar(usuarioRegistro));
        }

        [HttpPost]
        [Route("detran/autenticar")]
        public async Task<ActionResult> Login(UsuarioLoginDTO usuarioLogin)
        {
            return CustomResponse(await _identidadeService.Login(usuarioLogin));
        }
    }
}