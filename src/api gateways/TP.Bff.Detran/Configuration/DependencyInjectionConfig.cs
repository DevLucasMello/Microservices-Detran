using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TP.WebAPI.Core.Usuario;

namespace TP.Bff.Detran.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
        }
    }
}