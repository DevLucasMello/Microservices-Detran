using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.Bff.Detran.Models.Identidade;
using TP.Core.Communication;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface IIdentidadeService
    {
        Task<UsuarioRespostaLogin> Registrar(UsuarioRegistroDTO usuarioRegistro);
        Task<UsuarioRespostaLogin> Login(UsuarioLoginDTO usuarioLogin);
    }


    public class IdentidadeService : Service, IIdentidadeService
    {
        private readonly HttpClient _httpClient;

        public IdentidadeService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.IdentidadeUrl);
        }

        public async Task<UsuarioRespostaLogin> Registrar(UsuarioRegistroDTO usuarioRegistro)
        {
            var itemContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta/", itemContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLoginDTO usuarioLogin)
        {
            var itemContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar/", itemContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
