using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("Info", Namespace = "https://api.101domain.com")]
    public class InfoResponse
    {
        [XmlElement("Domain")]
        public string Domain;

        [XmlElement("Create")]
        public DateTime Create;

        [XmlElement("Expiration")]
        public DateTime Expiration;

        [XmlArray("StatusFlags")]
        [XmlArrayItem("Status")]
        public string[] StatusFlags;

        [XmlElement("Contacts")]
        public ContactsPlain Contacts;

        [XmlArrayItem("Host")]
        public string[] NameServers;

        public InfoResponse()
        {
        }
    }

    [XmlRoot("Register", Namespace = "https://api.101domain.com")]
    public class RegisterResponse : InfoResponse
    {
    }
}
