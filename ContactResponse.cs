using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ContactInfo", Namespace = "https://api.101domain.com")]
    public class ContactResponse
    {
        [XmlElement("id")]
        public string id;

        [XmlElement("Contact")]
        public Contact Contact;

        public ContactResponse()
        {
        }
    }
}
