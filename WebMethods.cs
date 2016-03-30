using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace _101DomainsAPI
{
    internal class WebMethods
    {
        private const string DOC_TEMPLATE = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><DOMAPI xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"https://api.101domain.com api.xsd\" xmlns=\"https://api.101domain.com\">" +
                                            "<Request><Login><id>{0}</id><Key>{1}</Key></Login>{2}</Request></DOMAPI>";
        private WebClient client;
        private string baseUrl;
        private string login;
        private string password;

        public WebMethods(WebClient client, string baseUrl, string login, string password)
        {
            this.client = client;
            this.baseUrl = baseUrl;
            this.login = login;
            this.password = password;
        }

        public virtual string Post(string data)
        {
            int pos = data.IndexOf("\r\n");
            data = data.Substring(pos + 2).Replace("\r\n","");
            string request = string.Format(DOC_TEMPLATE, login, password, data);
            return MakeRemoteCall((client) =>
            {
                var result = client.UploadString(baseUrl, "POST", request);
                return result;
            });
        }

        private string MakeRemoteCall(Func<WebClient, string> remoteCall)
        {
            try
            {
                return remoteCall.Invoke(client);
            }
            catch (WebException e)
            {
                string[] keys = e.Response.Headers.AllKeys;
                if (Array.IndexOf(keys, "x-error-message") != -1)
                {
                    string error = e.Response.Headers["x-error-message"];
                    throw new Exception(error);
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}

