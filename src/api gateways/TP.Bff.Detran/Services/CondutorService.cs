using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.Bff.Detran.Models.Condutor;
using TP.Core.Communication;
using TP.Core.DomainObjects;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface ICondutorService
    {
        Task<PagedResult<CondutorDTO>> ObterTodosCondutores(int pageSize, int pageIndex, string query);
        Task<PagedResult<CondutorDTO>> ObterCondutoresPorPlaca(int pageSize, int pageIndex, string placa);
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

        public async Task<PagedResult<CondutorDTO>> ObterTodosCondutores(int pageSize, int pageIndex, string query)
        {
            var response = await _httpClient.GetAsync($"/condutor?ps={pageSize}&page={pageIndex}&q={query}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<PagedResult<CondutorDTO>>(response);
        }

        public async Task<PagedResult<CondutorDTO>> ObterCondutoresPorPlaca(int pageSize, int pageIndex, string placa)
        {
            var response = await _httpClient.GetAsync($"/condutor/placa?ps={pageSize}&page={pageIndex}&placa={placa}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<PagedResult<CondutorDTO>>(response);
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
