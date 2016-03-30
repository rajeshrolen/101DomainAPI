using System.Xml.Serialization;

namespace _101DomainsAPI
{
    /// <summary>
    /// Container for domain update
    /// </summary>
    [XmlRoot("Update")]
    public class DomainForUpdate
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";


        [XmlElement("Domain")]
        public string Domain;

        [XmlElement("Contacts")]
        public Contacts Contacts;

        [XmlArrayItem("Host")]
        public string[] NameServers;

        public DomainForUpdate()
        {
        }

        public DomainForUpdate(string domain, string[] nameservers)
        {
            Domain = domain;
            this.Contacts = new Contacts();
            NameServers = nameservers;
        }
    }
}
