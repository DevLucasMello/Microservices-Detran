using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using TP.Bff.Detran.Extensions;
using TP.WebAPI.Core.Http;

namespace TP.Bff.Detran.Services
{
    public interface ICondutorService
    {

    }

    public class CondutorService : Service, ICondutorService
    {
        private readonly HttpClient _httpClient;

        public CondutorService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CondutorUrl);
        }
    }
}
