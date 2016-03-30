using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ContactInfo")]
    public class ContactForRequest
    {
        [XmlElement("id")]
        public string id;

        public ContactForRequest()
        {
        }

        public ContactForRequest(string _id)
        {
            id = _id;
        }
    }
}
