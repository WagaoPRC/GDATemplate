using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace ProdespGDA.Commom.Models
{
    //[JsonConverter(typeof(StringValuedEnumConverter))]
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD,
        OPTIONS,
    }

    /// <summary>
    /// Helper for the enum type HttpMethod
    /// </summary>
    public static class HttpMethodHelper
    {
        //string values corresponding the enum elements
        private static List<string> stringValues = new List<string> { "GET", "POST", "PUT", "DELETE", "HEAD", "OPTIONS" };

        /// <summary>
        /// Converts a HttpMethod value to a corresponding string value
        /// </summary>
        /// <param name="enumValue">The HttpMethod value to convert</param>
        /// <returns>The representative string value</returns>
        public static string ToValue(HttpMethod enumValue)
        {
            switch (enumValue)
            {
                //only valid enum elements can be used
                //this is necessary to avoid errors
                case HttpMethod.GET:
                case HttpMethod.POST:
                case HttpMethod.PUT:
                case HttpMethod.DELETE:
                case HttpMethod.HEAD:
                case HttpMethod.OPTIONS:
                    return stringValues[(int)enumValue];

                //an invalid enum value was requested
                default:
                    return null;
            }
        }

        /// <summary>
        /// Convert a list of HttpMethod values to a list of strings
        /// </summary>
        /// <param name="enumValues">The list of HttpMethod values to convert</param>
        /// <returns>The list of representative string values</returns>
        public static List<string> ToValue(List<HttpMethod> enumValues)
        {
            if (null == enumValues)
                return null;

            return enumValues.Select(eVal => ToValue(eVal)).ToList();
        }

        /// <summary>
        /// Converts a string value into HttpMethod value
        /// </summary>
        /// <param name="value">The string value to parse</param>
        /// <returns>The parsed HttpMethod value</returns>
        public static HttpMethod ParseString(string value)
        {
            int index = stringValues.IndexOf(value);
            if (index < 0)
                throw new InvalidCastException(string.Format("Unable to cast value: {0} to type HttpMethod", value));

            return (HttpMethod)index;
        }
    }
}