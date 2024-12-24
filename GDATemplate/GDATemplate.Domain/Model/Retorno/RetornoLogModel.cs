using System.Collections.Generic;
using System.Net;

namespace GDATemplate.Domain.Model.Retorno
{
    public class RetornoLogModel
    {
        public string Controller { get; set; }
        public string Metodo { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Mensagem { get; set; }

        public Dictionary<string, string> Parametros { get; set; }

    }
}
