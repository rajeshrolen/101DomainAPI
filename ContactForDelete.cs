//===============================================
//this lib is developed by Dr. Rajesh Kumar Rolen
//===============================================
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ContactDelete")]
    public class ContactForDelete
    {
        [XmlElement("id")]
        public string id;

        public ContactForDelete()
        {
        }

        public ContactForDelete(string _id)
        {
            id = _id;
        }
    }
}
