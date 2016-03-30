using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI.Domain
{
    [XmlRoot("Check")]
    public class Check
    {
        [XmlElement("Domain")]
        public DomainForCheck[] Domains;

        [XmlAttribute("requestId")]
        public string requestId = "1";

        public Check()
        {
            Domains = null;
        }

        public Check(DomainForCheck[] domains)
        {
            Domains = domains;
        }
    }
}
