namespace HIAE.SIAF.Notificacoes.API.Models
{
    /// <summary>
    /// Model de resposta
    /// </summary>
    public class RespostaModel
    {
        public int CodigoMensagem { get; set; }

        public string LocalErro { get; set; }

        public string Mensagem { get; set; }

        public string MensagemInterna { get; set; }

        public int Status { get; set; }
    }
}
