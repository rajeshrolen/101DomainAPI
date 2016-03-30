using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _101DomainsAPI.Domain;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace _101DomainsAPI
{
    public class Domains
    {
        private WebMethods m_engine;

        public Domains(string url, string login, string password)
        {
            m_engine = new WebMethods(new System.Net.WebClient(), url, login, password);
        }

        public string CreateContact(Contact contact)
        {
            XmlDocument result = execute(contact);
            return extractSingle(result, "id");
        }

        public void UpdateContact(string id, Contact contactInfo)
        {
            ContactForUpdate contact = new ContactForUpdate(id, contactInfo);
            XmlDocument result = execute(contact);
        }

        public void DeleteContact(string id)
        {
            ContactForDelete contact = new ContactForDelete(id);
            XmlDocument result = execute(contact);
        }

        public Contact GetContact(string id)
        {
            ContactForRequest contact = new ContactForRequest(id);
            XmlDocument result = execute(contact);

            ContactResponse contactResponse = extractObject<ContactResponse>(result);
            return contactResponse.Contact;
        }

        public CheckResponse.CheckedDomain[] CheckDomains(DomainForCheck[] domains)
        {
            Check check = new Check(domains);
            XmlDocument result = execute(check);
            CheckResponse response = extractObject<CheckResponse>(result);
            return response.Domains;
        }

        public RegisterResponse RegisterDomain(DomainForRegister domain)
        {
            XmlDocument result = execute(domain);
            RegisterResponse response = extractObject<RegisterResponse>(result);
            return response;
        }

        public void RenewDomain(DomainForRenew domain)
        {
            XmlDocument result = execute(domain);
        }

        public InfoResponse InfoDomain(DomainForCheck domain)
        {
            Info info = new Info(domain);
            XmlDocument result = execute(info);

            InfoResponse response = extractObject<InfoResponse>(result);
            return response;
        }

        public void UpdateDomain(DomainForUpdate domain)
        {
            XmlDocument result = execute(domain);
        }

        public ZoneListResponse.ZoneInfo[] ZoneList()
        {
            XmlDocument result = execute(new ZoneList());
            ZoneListResponse response = extractObject<ZoneListResponse>(result);
            return response.Zones;
        }

        public ZoneInfoResponse ZoneInfo(string zone)
        {
            XmlDocument result = execute(new ZoneInfo(zone));
            ZoneInfoResponse response = extractObject<ZoneInfoResponse>(result);
            return response;
        }

        private XmlDocument execute<RequestType>(RequestType data) where RequestType : class
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                    XmlSerializer ser = new XmlSerializer(typeof(RequestType));
                    xns.Add(string.Empty, string.Empty);
                    ser.Serialize(writer, data, xns);

                    stream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = m_engine.Post(reader.ReadToEnd());
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(result);
                        checkError(doc);
                        return doc;
                    }
                }
            }
        }

        public void checkError(XmlDocument doc)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("e", "https://api.101domain.com");

            XmlNode errorNode = doc.SelectSingleNode("/e:DOMAPI/e:Response/*[@errorMessage and @errorCode]", manager);
            if (errorNode != null)
            {
                string message = errorNode.Attributes["errorMessage"].Value;

                string code = errorNode.Attributes["errorCode"].Value;
                if (code != "1000" && code != "1001")
                {
                    throw new Exception(message);
                }
            }
            else
            {
                throw new Exception("wrong response");
            }
        }

        public string extractSingle(XmlDocument doc, string singleName)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("e", "https://api.101domain.com");

            XmlNode errorNode = doc.SelectSingleNode("/e:DOMAPI/e:Response/*/e:"+singleName, manager);
            if (errorNode != null)
            {
                return errorNode.InnerText;
            }
            return null;
        }

        public ObjectType extractObject<ObjectType>(XmlDocument doc) where ObjectType : class
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
            manager.AddNamespace("e", "https://api.101domain.com");

            XmlNode errorNode = doc.SelectSingleNode("/e:DOMAPI/e:Response", manager);
            if (errorNode != null)
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(errorNode.InnerXml)))
                {
                    XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                    XmlSerializer ser = new XmlSerializer(typeof(ObjectType));
                    xns.Add(string.Empty, string.Empty);
                    ObjectType o = ser.Deserialize(stream) as ObjectType;
                    return o;
                }
            }
            return null;
        }

    }
}
