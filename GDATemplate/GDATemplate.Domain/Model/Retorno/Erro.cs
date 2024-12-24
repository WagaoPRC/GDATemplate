using System.Net;

namespace GDATemplate.Domain.Model.Retorno
{
    public class Erro
    {
        public HttpStatusCode Id { get; set; }

        public string Detalhes { get; set; }
    }
}
