using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface IIdentidadeService
    {

    }


    public class IdentidadeService : Service, IIdentidadeService
    {
        private readonly HttpClient _httpClient;

        public IdentidadeService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.IdentidadeUrl);
        }
    }
}
