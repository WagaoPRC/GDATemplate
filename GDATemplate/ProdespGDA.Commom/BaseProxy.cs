using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using ProdespGDA.Commom.Exceptions;
using ProdespGDA.Commom.Http.Client;
using ProdespGDA.Commom.Http.Request;
using ProdespGDA.Commom.Http.Response;
using ProdespGDA.Commom.Models;
using ProdespGDA.Commom.Utilities;

namespace ProdespGDA.Commom;

public class BaseProxy
{
    protected ArrayDeserialization ArrayDeserializationFormat = ArrayDeserialization.Plain;
    protected static char ParameterSeparator = '&';



    /// <summary>
    /// User-Agent header value
    /// </summary>
    public string userAgent = "PostmanRuntime/7.29.2";

    public string _baseUri { get; internal set; }

    /// <summary>
    /// HttpClient instance
    /// </summary>
    public HttpClientWrapper httpClient { get; internal set; }

    /// <summary>
    /// Contructor to initialize the controller with the specified configuration and HTTP callback
    /// </summary>
    /// <param name="config">Configuration for the API</param>
    public BaseProxy(string baseUri)
    {
        this.httpClient = new HttpClientWrapper(new System.Net.Http.HttpClient());

        _baseUri = baseUri;
    }

    /// <summary>
    /// Create JSON-encoded multipart content from input.
    /// </summary>
    internal static MultipartContent CreateJsonEncodedMultipartContent(object input, Dictionary<string, IReadOnlyCollection<string>> headers)
    {
        return new MultipartByteArrayContent(Encoding.ASCII.GetBytes(APIHelper.JsonSerialize(input)), headers);
    }

    /// <summary>
    /// Create binary multipart content from file.
    /// </summary>
    internal static MultipartContent CreateFileMultipartContent(FileStreamInfo input, Dictionary<string, IReadOnlyCollection<string>> headers = null)
    {
        if (headers == null)
        {
            return new MultipartFileContent(input);
        }
        else
        {
            return new MultipartFileContent(input, headers);
        }
    }

    /// <summary>
    /// Get default HTTP client instance
    /// </summary>
    public IHttpClient GetClientInstance()
    {
        return httpClient;
    }

    /// <summary>
    /// Validates the response against HTTP errors defined at the API level
    /// </summary>
    /// <param name="_response">The response recieved</param>
    /// <param name="_context">Context of the request and the recieved response</param>
    public void ValidateResponse(HttpStringResponse _response, HttpContext _context)
    {
        var a = _response;

        if ((_response.StatusCode == 200) ||
            (_response.StatusCode == 202) ||
            (_response.StatusCode == 208) ||
            (_response.StatusCode == 402) ||
            (_response.StatusCode == 425) ||
            (_response.StatusCode == 424))   //[200, 208] = HTTP OK
        {
            return;
        }
        else
        {
            throw new APIException(_response.Body == "" ? @"HTTP Response Not OK" : _response.Body, _context);
        }
    }

    public HttpRequest BeforeCall<T>(string endpoint, HttpMethod httpMethod, T body, string authorization)
    {

        //prepare query string for API call
        StringBuilder _queryBuilder = new StringBuilder(_baseUri);
        _queryBuilder.Append(endpoint);

        //validate and preprocess url
        string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

        //append request with appropriate headers and parameters
        var _headers = new Dictionary<string, string>()
        {
            { "user-agent", userAgent },
            { "content-type", "application/json; charset=utf-8" },
            { "Authorization", "Bearer " + authorization }
        };


        //append body params
        var _body = APIHelper.JsonSerialize(body);

        //prepare the API call request to fetch the response

        if (httpMethod == HttpMethod.GET)
            return GetClientInstance().Get(_queryUrl, _headers);
        else if (httpMethod == HttpMethod.POST && String.IsNullOrEmpty(_body))
            return GetClientInstance().Post(_queryUrl, _headers, null);
        else if (httpMethod == HttpMethod.POST)
            return GetClientInstance().PostBody(_queryUrl, _headers, _body);
        else if (httpMethod == HttpMethod.PUT)
            return GetClientInstance().PutBody(_queryUrl, _headers, _body);
        else if (httpMethod == HttpMethod.DELETE && String.IsNullOrEmpty(_body))
            return GetClientInstance().Delete(_queryUrl, _headers, null);
        else if (httpMethod == HttpMethod.DELETE)
            return GetClientInstance().DeleteBody(_queryUrl, _headers, _body);
        else
            throw new ArgumentException($"Unsupported HttpMethod - {httpMethod}", "httpMethod");
    }

    public HttpRequest BeforeCall(string endpoint, HttpMethod httpMethod, string authorization)
    {

        //prepare query string for API call
        StringBuilder _queryBuilder = new StringBuilder(_baseUri);
        _queryBuilder.Append(endpoint);

        //process optional query parameters
        APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>(), ArrayDeserializationFormat, ParameterSeparator);

        //validate and preprocess url
        string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

        //append request with appropriate headers and parameters
        var _headers = new Dictionary<string, string>()
        {
            { "user-agent", userAgent },
            { "content-type", "application/json; charset=utf-8" },
            { "Authorization", "Bearer " + authorization }
        };

        //prepare the API call request to fetch the response
        if (httpMethod == HttpMethod.GET)
            return GetClientInstance().Get(_queryUrl, _headers);
        else if (httpMethod == HttpMethod.POST)
            return GetClientInstance().Post(_queryUrl, _headers, null);
        else if (httpMethod == HttpMethod.PUT)
            return GetClientInstance().Put(_queryUrl, _headers, null);
        else if (httpMethod == HttpMethod.DELETE)
            return GetClientInstance().Delete(_queryUrl, _headers, null);
        else
            throw new ArgumentException($"Unsupported HttpMethod - {httpMethod}", "httpMethod");
    }

    public HttpRequest BeforeCallAuth(string endpoint, HttpMethod httpMethod, string authorization, string _body)
    {
        //prepare query string for API call
        //StringBuilder _queryBuilder = new StringBuilder(_baseUri);
        //_queryBuilder.Append(endpoint);

        //process optional query parameters
        // APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>(), ArrayDeserializationFormat, ParameterSeparator);

        //validate and preprocess url
        //string _queryUrl = APIHelper.CleanUrl(_queryBuilder);
        string _queryUrl = endpoint;
        try
        {
            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            {
                //{ "user-agent", userAgent },
                { "content-type", "application/x-www-form-urlencoded" },
                { "Authorization", "Basic " + authorization }
            };

            //prepare the API call request to fetch the response
            if (httpMethod == HttpMethod.GET)
                return GetClientInstance().Get(_queryUrl, _headers);
            else if (httpMethod == HttpMethod.POST && String.IsNullOrEmpty(_body))
                return GetClientInstance().Post(_queryUrl, _headers, null);
            else if (httpMethod == HttpMethod.POST)
                return GetClientInstance().PostBody(_queryUrl, _headers, _body);
            else if (httpMethod == HttpMethod.PUT)
                return GetClientInstance().PutBody(_queryUrl, _headers, _body);
            else if (httpMethod == HttpMethod.DELETE && String.IsNullOrEmpty(_body))
                return GetClientInstance().Delete(_queryUrl, _headers, null);
            else if (httpMethod == HttpMethod.DELETE)
                return GetClientInstance().DeleteBody(_queryUrl, _headers, _body);
            else
                throw new ArgumentException($"Unsupported HttpMethod - {httpMethod}", "httpMethod");
        }
        catch (Exception)
        {
            throw;
        }
    }



    public HttpRequest BeforeCallSOAPXML<T>(string endpoint, string authorization, HttpMethod httpMethod, T body, XmlSerializerNamespaces EnvToSerialize, Dictionary<string, string> _headers = null)
    {

        string _queryUrl = endpoint;
        try
        {
            //append request with appropriate headers and parameters
            if (_headers == null)// && (endpoint == "https://webservices.havail.sabre.com" || endpoint == "https://sws-crt.cert.havail.sabre.com"))
            {
                _headers = new Dictionary<string, string>()
            {
                { "content-type", "text/xml;charset=\"utf-8\"" },
                { "user-agent", userAgent},
                { "content-length", "<calculated when request is sent>"},
                { "host", "<calculated when request is sent>"},
                { "Accept", "text/xml"},
                { "Connection","Keep-Alive"}
                };
            }


            //append body params
            var _body = APIHelper.XMLSerialize<T>(body, EnvToSerialize);

            if (httpMethod == HttpMethod.GET)
                return GetClientInstance().Get(_queryUrl, _headers);
            else if (httpMethod == HttpMethod.POST && String.IsNullOrEmpty(_body))
                return GetClientInstance().Post(_queryUrl, _headers, null);
            else if (httpMethod == HttpMethod.POST)
                return GetClientInstance().PostBody(_queryUrl, _headers, _body);
            else if (httpMethod == HttpMethod.PUT)
                return GetClientInstance().PutBody(_queryUrl, _headers, _body);
            else if (httpMethod == HttpMethod.DELETE && String.IsNullOrEmpty(_body))
                return GetClientInstance().Delete(_queryUrl, _headers, null);
            else if (httpMethod == HttpMethod.DELETE)
                return GetClientInstance().DeleteBody(_queryUrl, _headers, _body);
            else
                throw new ArgumentException($"Unsupported HttpMethod - {httpMethod}", "httpMethod");
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task Delete(string url, CancellationToken cancellationToken = default)
    {
        await Delete<object>(url, new object(), null, cancellationToken);
    }

    public async Task Delete<T>(string url, T obj, string authorization, CancellationToken cancellationToken = default)
    {
        //prepare the API call request to fetch the response
        HttpRequest _request = this.BeforeCall<T>(url, HttpMethod.DELETE, obj, authorization);

        //invoke request and get response
        HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
        HttpContext _context = new HttpContext(_request, _response);
        //handle errors defined at the API level
        ValidateResponse(_response, _context);
    }

    public async Task<T> Get<T>(string url, CancellationToken cancellationToken, string authorization)
    {
        var _request = this.BeforeCall(url, HttpMethod.GET, authorization);

        var _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
        HttpContext _context = new HttpContext(_request, _response);
        ValidateResponse(_response, _context);

        return APIHelper.JsonDeserialize<T>(_response.Body);
    }

    public async Task<U> Post<U>(string url, CancellationToken cancellationToken = default)
    {
        return await Post<object, U>(url, new object(), null, cancellationToken);
    }

    public async Task<U> Post<T, U>(string url, T body, CancellationToken cancellationToken = default)
    {
        return await Post<object, U>(url, body, null, cancellationToken);
    }

    public async Task<U> Post<U>(string url, string authorization, CancellationToken cancellationToken = default)
    {
        return await Post<object, U>(url, new object(), authorization, cancellationToken);
    }

    public async Task<U> Post<T, U>(string url, T body, string authorization, CancellationToken cancellationToken = default)
    {
        var _request = this.BeforeCall<T>(url, HttpMethod.POST, body, authorization);

        var _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
        HttpContext _context = new HttpContext(_request, _response);
        ValidateResponse(_response, _context);

        return APIHelper.JsonDeserialize<U>(_response.Body);
    }
}
