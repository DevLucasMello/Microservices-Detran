using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.Bff.Detran.Models.Veiculo;
using TP.Core.Communication;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<VeiculoDTO>> ObterTodosVeiculos();
        Task<IEnumerable<VeiculoDTO>> ObterVeiculosPorCPF(string cpf);
        Task<VeiculoDTO> ObterVeiculoPorId(Guid id);
        Task<ResponseResult> AdicionarVeiculo(AdicionarVeiculoDTO veiculo);
        Task<ResponseResult> AtualizarVeiculo(Guid id, AtualizarVeiculoDTO veiculo);
        Task<ResponseResult> ExcluirVeiculo(Guid id);
    }
    
    public class VeiculoService : Service, IVeiculoService
    {
        private readonly HttpClient _httpClient;

        public VeiculoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.VeiculoUrl);
        }

        public async Task<IEnumerable<VeiculoDTO>> ObterTodosVeiculos()
        {
            var response = await _httpClient.GetAsync("/veiculo/");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<IEnumerable<VeiculoDTO>>(response);
        }

        public async Task<IEnumerable<VeiculoDTO>> ObterVeiculosPorCPF(string cpf)
        {
            var response = await _httpClient.GetAsync($"/veiculo/documento/{cpf}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<IEnumerable<VeiculoDTO>>(response);
        }

        public async Task<VeiculoDTO> ObterVeiculoPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/veiculo/{id}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<VeiculoDTO>(response);
        }

        public async Task<ResponseResult> AdicionarVeiculo(AdicionarVeiculoDTO veiculo)
        {
            var itemContent = ObterConteudo(veiculo);

            var response = await _httpClient.PostAsync("/veiculo/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> AtualizarVeiculo(Guid id, AtualizarVeiculoDTO veiculo)
        {
            var itemContent = ObterConteudo(veiculo);

            var response = await _httpClient.PutAsync($"/veiculo/{id}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> ExcluirVeiculo(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/veiculo/{id}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }
    }
}
