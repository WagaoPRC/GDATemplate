using System.Net;

namespace GDATemplate.Domain.Model.Retorno
{
    public class Retorno
    {
        public HttpStatusCode Id { get; set; }

        public string Mensagem { get; set; }
    }
}
