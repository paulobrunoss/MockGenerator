namespace HIAE.SIAF.Notificacoes.API.Models
{
    /// <summary>
    /// Model de retorno da API
    /// </summary>
    public class ResultModel
    {
        public string rota { get; set; }

        public int IdTabela { get; set; }

        public object Parametros { get; set; }

        public object[] AcaoUI { get; set; }

        public object[] Contexto { get; set; }

        public object Objeto { get; set; }

        public RespostaModel[] Resposta { get; set; }

        public int Status { get; set; }
    }
}
