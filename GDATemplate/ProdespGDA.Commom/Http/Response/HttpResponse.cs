using System;
using System.Collections.Generic;
using System.IO;

namespace ProdespGDA.Commom.Http.Response
{
    public class HttpResponse
    {
        /// <summary>
        /// HTTP Status code of the http response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Headers of the http response
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Stream of the body
        /// </summary>
        public Stream RawBody { get; set; }

        public HttpResponse(int statusCode, Dictionary<string, string> headers, Stream rawBody)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.RawBody = rawBody;
        }
    }
}