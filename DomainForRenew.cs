using System.Xml.Serialization;

namespace _101DomainsAPI
{
    /// <summary>
    /// Container for domains renewals
    /// </summary>
    [XmlRoot("Renew")]
    public class DomainForRenew
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";

        [XmlElement("Domain")]
        public string Domain;

        [XmlElement("Period")]
        public int Period;

        public DomainForRenew()
        {
        }

        public DomainForRenew(string domain, int period)
        {
            Domain = domain;
            Period = period;
        }
    }
}
