using System.Xml.Serialization;

namespace _101DomainsAPI
{
    public sealed class Contacts
    {
        [XmlElement("Registrant")]
        public ContactId Registrant;

        [XmlElement("Admin")]
        public ContactId Admin;

        [XmlElement("Technical")]
        public ContactId Technical;

        [XmlElement("Billing")]
        public ContactId Billing;
    }

    public sealed class ContactsPlain
    {
        [XmlElement("Registrant")]
        public string Registrant;

        [XmlElement("Admin")]
        public string Admin;

        [XmlElement("Technical")]
        public string Technical;

        [XmlElement("Billing")]
        public string Billing;
    }
}
