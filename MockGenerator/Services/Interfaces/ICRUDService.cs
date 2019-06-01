using HIAE.SIAF.Notificacoes.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockGenerator.Services.Interfaces
{
    public interface ICRUDService
    {
        Task<ResultModel> Post(dynamic body, string api, string metodo, string idSessao, string token);

        Task<List<dynamic>> Get(string query, string api, string metodo, string idSessao, string token);
    }
}
