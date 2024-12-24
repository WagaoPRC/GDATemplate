using System;
using System.Collections.Generic;
using System.Text;

namespace ProdespGDA.Commom
{
    public class DigitalConnectCheckinOptions
    {
        public DigitalConnectCheckinOptions()
        {

        }

        //A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Server, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Server, string>>
        {
            {
                Environment.Default, new Dictionary<Server, string>
                {
                    { Server.Default, "https://{host_and_port}/{api_context_path}" },
                    { Server.Auth, "https://{host_and_port}{auth_context_path}" },
                }
            },
            {
                Environment.Mock, new Dictionary<Server, string>
                {
                    { Server.Default, "https://f7odr7msbeqckh59c-mock.stoplight-proxy.io" },
                    { Server.Auth, "https://f7odr7msbeqckh59c-mock.stoplight-proxy.io" },
                }
            },
        };
        private List<KeyValuePair<string, object>> GetBaseUriParameters()
        {
            List<KeyValuePair<string, object>> kvpList = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("host_and_port", HostAndPort),
                new KeyValuePair<string, object>("api_context_path", ApiContextPath),
                new KeyValuePair<string, object>("auth_context_path", AuthContextPath),
            };
            return kvpList;
        }
        public Environment Environment { get; set; }

        public string HostAndPort { get; set; }

        public string ApiContextPath { get; set; }

        public string AuthContextPath { get; set; }

        public string GetBaseUri(Server alias = Server.Default)
        {
            {
                StringBuilder Url = new StringBuilder(EnvironmentsMap[Environment][alias]);
                Utilities.APIHelper.AppendUrlWithTemplateParameters(Url, GetBaseUriParameters());
                return Url.ToString();
            }
        }
    }
}
