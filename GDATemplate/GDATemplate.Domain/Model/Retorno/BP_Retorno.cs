using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace GDATemplate.Domain.Model.Retorno
{
    public class BP_Retorno
    {
        public BP_Retorno(string service = "", string description = "")
        {

            Cabecalho = new Cabecalho()
            {
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Detalhes = description,
                Servico = service,
            };

            Status = new Retorno();
            Erros = new List<Erro>();
            AddRetorno(HttpStatusCode.OK, HttpStatusCode.OK.ToString());
        }

        [JsonProperty(Order = 1)]
        public Cabecalho Cabecalho { get; set; }

        [JsonProperty(Order = 2)]
        public Retorno Status { get; set; }

        [JsonProperty(Order = 3)]
        public List<Erro> Erros { get; set; }

        public void AddErro(HttpStatusCode statusCode, string DS_ERRO)
        {
            Erros.Add(new Erro() { Id = statusCode, Detalhes = DS_ERRO });
            AddRetorno(statusCode, statusCode.ToString());

        }

        public void AddRetorno(HttpStatusCode statusCode, string DS_RETORNO)
        {
            Status.Id = statusCode;
            Status.Mensagem = DS_RETORNO;
        }
    }



    public class BP_Retorno<T>
    {
        public BP_Retorno(string service = "", string description = "")
        {
            Cabecalho = new Cabecalho()
            {
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Detalhes = description,
                Servico = service,
            };

            Status = new Retorno();
            Erros = new List<Erro>();
            AddRetorno(HttpStatusCode.OK, HttpStatusCode.OK.ToString());
        }

        [JsonProperty(Order = 1)]
        public Cabecalho Cabecalho { get; set; }

        [JsonProperty(Order = 2)]
        public Retorno Status { get; set; }

        [JsonProperty(Order = 3)]
        public List<Erro> Erros { get; set; }

        [JsonProperty(Order = 4)]
        public IEnumerable<T> Tabela { get; set; }

        public void AddErro(HttpStatusCode statusCode, string DS_ERRO)
        {
            Erros.Add(new Erro() { Id = statusCode, Detalhes = DS_ERRO });
            AddRetorno(statusCode, statusCode.ToString());
        }

        public void AddRetorno(HttpStatusCode statusCode, string DS_RETORNO)
        {
            Status.Id = statusCode;
            Status.Mensagem = DS_RETORNO;
        }
    }

    public class BP_RetornoObjeto<T>
    {
        public BP_RetornoObjeto(string service = "", string description = "")
        {
            Cabecalho = new Cabecalho()
            {
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Detalhes = description,
                Servico = service,
            };

            Status = new Retorno();
            Erros = new List<Erro>();
            AddRetorno(HttpStatusCode.OK, HttpStatusCode.OK.ToString());
        }

        [JsonProperty(Order = 1)]
        public Cabecalho Cabecalho { get; set; }

        [JsonProperty(Order = 2)]
        public Retorno Status { get; set; }

        [JsonProperty(Order = 3)]
        public T Dados { get; set; }

        [JsonProperty(Order = 4)]
        public List<Erro> Erros { get; set; }

        public void AddErro(HttpStatusCode statusCode, string DS_ERRO)
        {
            Erros.Add(new Erro() { Id = statusCode, Detalhes = DS_ERRO });
            AddRetorno(statusCode, statusCode.ToString());
        }

        public void AddRetorno(HttpStatusCode statusCode, string DS_RETORNO)
        {
            Status.Id = statusCode;
            Status.Mensagem = DS_RETORNO;
        }
    }
}
