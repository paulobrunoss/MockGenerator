using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockGenerator.Services;
using MockGenerator.Services.Interfaces;
using System;
using System.Net.Http.Headers;

namespace HIAE.SIAF.Notificacoes.API
{
    /// <summary>
    /// Realiza a configuração de injeção de dependencias
    /// </summary>
    public class InjecaoDependencia
    {
        /// <summary>
        /// Realiza a configuração de injeção de dependencias
        /// </summary>
        public static void Configurar(IConfiguration configuration, IServiceCollection services)
        {
            services.AddHttpClient<ICRUDService, ACRUDService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:38100/SiafGateway/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        }
    }
}
