using System.Xml.Serialization;

namespace _101DomainsAPI.Domain
{
    [XmlRoot("Info")]
    public class Info
    {
        [XmlElement("Domain")]
        public DomainForCheck Domain;

        [XmlAttribute("requestId")]
        public string requestId = "1";

        public Info()
        {
            Domain = null;
        }

        public Info(DomainForCheck domain)
        {
            Domain = domain;
        }
    }
}
