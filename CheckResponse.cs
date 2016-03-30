//===============================================
//this lib is developed by Dr. Rajesh Kumar Rolen
//===============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("Check", Namespace = "https://api.101domain.com")]
    public sealed class CheckResponse
    {
        public sealed class CheckedDomain
        {
            [XmlAttribute("avail")]
            public bool Avail;

            [XmlAttribute("reason")]
            public string Reason;

            [XmlText()]
            public string Name;
        }

        [XmlElement("Domain")]
        public CheckedDomain[] Domains;

        public CheckResponse()
        {
        }
    }
}
