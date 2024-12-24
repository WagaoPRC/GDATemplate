using System;
using System.Collections.Generic;
using System.IO;

namespace ProdespGDA.Commom.Http.Response
{
    public class HttpStringResponse : HttpResponse
    {
        /// <summary>
        /// Raw string body of the http response
        /// </summary>
        public string Body { get; set; }

        public HttpStringResponse(int statusCode, Dictionary<string, string> headers, Stream rawBody, string body) : base(statusCode, headers, rawBody)
        {
            this.Body = body;
        }
    }
}
