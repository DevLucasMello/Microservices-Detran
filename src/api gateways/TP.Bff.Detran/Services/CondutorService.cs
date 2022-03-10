using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.Bff.Detran.Models.Condutor;
using TP.Core.Communication;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface ICondutorService
    {
        Task<IEnumerable<CondutorDTO>> ObterTodosCondutores();
        Task<IEnumerable<CondutorDTO>> ObterCondutoresPorPlaca(string placa);
        Task<CondutorDTO> ObterCondutorPorId(Guid id);
        Task<CondutorDTO> ObterCondutorPorCpf(string cpf);
        Task<ResponseResult> AdicionarCondutor(AdicionarCondutorDTO condutor);
        Task<ResponseResult> AtualizarCondutor(Guid id, AtualizarCondutorDTO condutor);
        Task<ResponseResult> ExcluirCondutor(Guid id);

    }

    public class CondutorService : Service, ICondutorService
    {
        private readonly HttpClient _httpClient;

        public CondutorService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CondutorUrl);
        }

        public async Task<IEnumerable<CondutorDTO>> ObterTodosCondutores()
        {
            var response = await _httpClient.GetAsync("/condutor/");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<IEnumerable<CondutorDTO>>(response);
        }

        public async Task<IEnumerable<CondutorDTO>> ObterCondutoresPorPlaca(string placa)
        {
            var response = await _httpClient.GetAsync($"/condutor/placa/{placa}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<IEnumerable<CondutorDTO>>(response);
        }

        public async Task<CondutorDTO> ObterCondutorPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/condutor/{id}");           

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<CondutorDTO>(response);
        }

        public async Task<CondutorDTO> ObterCondutorPorCpf(string cpf)
        {
            var response = await _httpClient.GetAsync($"/condutor/documento/{cpf}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<CondutorDTO>(response);
        }

        public async Task<ResponseResult> AdicionarCondutor(AdicionarCondutorDTO condutor)
        {
            var itemContent = ObterConteudo(condutor);

            var response = await _httpClient.PostAsync("/condutor/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> AtualizarCondutor(Guid id, AtualizarCondutorDTO condutor)
        {
            var itemContent = ObterConteudo(condutor);

            var response = await _httpClient.PutAsync($"/condutor/{id}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> ExcluirCondutor(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/condutor/{id}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }
    }
}
