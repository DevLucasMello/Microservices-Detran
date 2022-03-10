using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TP.Bff.Detran.Models.Condutor;
using TP.Bff.Detran.Services;
using TP.WebAPI.Core.Controllers;

namespace TP.Bff.Detran.Controllers
{
    [Authorize]
    public class CondutorController : MainController
    {
        private readonly ICondutorService _condutorService;

        public CondutorController(ICondutorService condutorService)
        {
            _condutorService = condutorService;
        }

        [HttpGet]
        [Route("detran/condutor")]
        public async Task<IActionResult> ObterTodosCondutores()
        {
            return CustomResponse(await _condutorService.ObterTodosCondutores());
        }

        [HttpGet]
        [Route("detran/condutor/placa/{placa}")]
        public async Task<IActionResult> ObterCondutoresPorPlaca(string placa)
        {
            return CustomResponse(await _condutorService.ObterCondutoresPorPlaca(placa));
        }

        [HttpGet]
        [Route("detran/condutor/{id}")]
        public async Task<IActionResult> ObterCondutorPorId(Guid id)
        {
            return CustomResponse(await _condutorService.ObterCondutorPorId(id));
        }

        [HttpGet]
        [Route("detran/condutor/documento/{cpf}")]
        public async Task<IActionResult> ObterCondutorPorCpf(string cpf)
        {
            return CustomResponse(await _condutorService.ObterCondutorPorCpf(cpf));
        }

        [HttpPost]
        [Route("detran/condutor")]
        public async Task<IActionResult> AdicionarCondutor(AdicionarCondutorDTO condutor)
        {
            return CustomResponse(await _condutorService.AdicionarCondutor(condutor));            
        }

        [HttpPut]
        [Route("detran/condutor/{id}")]
        public async Task<IActionResult> AtualizarCondutor(Guid id, AtualizarCondutorDTO condutor)
        {
            var condutorExistente = await _condutorService.ObterCondutorPorId(id);
            if (condutorExistente is null)
            {
                AdicionarErroProcessamento("Condutor não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _condutorService.AtualizarCondutor(id, condutor));
        }

        [HttpDelete]
        [Route("detran/condutor/{id}")]
        public async Task<IActionResult> ExcluirCondutor(Guid id)
        {
            var condutorExistente = await _condutorService.ObterCondutorPorId(id);
            if (condutorExistente is null)
            {
                AdicionarErroProcessamento("Condutor não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _condutorService.ExcluirCondutor(id));
        }
    }
}