using HIAE.SIAF.Notificacoes.API.Models;
using MockGenerator.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MockGenerator.Services
{
    public class ACRUDService : ICRUDService
    {
        private readonly HttpClient httpClient;

        public ACRUDService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<ResultModel> Post(dynamic obj, string api, string metodo, string idSessao, string token)
        {
            httpClient.DefaultRequestHeaders.Add("idSessao", idSessao);
            httpClient.DefaultRequestHeaders.Add("token", token);

            var response = await httpClient
            .PostAsync($"{api}/{metodo}", new StringContent(JsonConvert.SerializeObject(obj)));

            ResultModel result = JsonConvert.DeserializeObject<ResultModel>(response.Content.ReadAsStringAsync().Result);
            result.rota = $"{api}/{metodo}";
            return result;
        }

        public async Task<List<dynamic>> Get(string query, string api, string metodo, string idSessao, string token)
        {
            httpClient.DefaultRequestHeaders.Add("idSessao", idSessao);
            httpClient.DefaultRequestHeaders.Add("token", token);

            var response = await httpClient.GetAsync($"{api}/{metodo}{query}");
            var result = JsonConvert.DeserializeObject<List<dynamic>>(response.Content.ReadAsStringAsync().Result);

            foreach (var obj in result)
            {
                obj.rota = $"{api}/{metodo}{query}";
            }
            
            return result;
        }
    }
}
