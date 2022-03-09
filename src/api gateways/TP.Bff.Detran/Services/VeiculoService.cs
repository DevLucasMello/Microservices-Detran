using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface IVeiculoService
    {

    }
    
    public class VeiculoService : Service, IVeiculoService
    {
        private readonly HttpClient _httpClient;

        public VeiculoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.VeiculoUrl);
        }
    }
}
