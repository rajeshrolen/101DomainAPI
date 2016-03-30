using System.Xml.Serialization;

namespace _101DomainsAPI
{
    /// <summary>
    /// Container for domains registration
    /// </summary>
    [XmlRoot("Register")]
    public class DomainForRegister
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";

        public sealed class Field
        {
            [XmlElement("Name")]
            public string Name;

            [XmlElement("Value")]
            public string Value;

            public Field()
            {
                Name = null;
                Value = null;
            }

            public Field(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }

        public sealed class UserAccountType
        {
            [XmlAttribute("create")]
            public bool create;

            [XmlElement("Email")]
            public string Email;
        }

        [XmlElement("Domain")]
        public string Domain;

        [XmlElement("Period")]
        public int Period;

        [XmlElement("Trustee")]
        public bool Trustee;

        [XmlElement("UserAccount")]
        public UserAccountType UserAccount;

        [XmlElement("Contacts")]
        public Contacts Contacts;

        [XmlArrayItem("Host")]
        public string[] NameServers;

        [XmlArrayItem("Field")]
        public Field[] Fields;

        public DomainForRegister()
        {
        }

        public DomainForRegister(string domain, int period, bool trustee, string registrantId, string[] nameservers)
        {
            Domain = domain;
            Period = period;
            Trustee = trustee;
            
            this.Contacts = new Contacts() { Registrant = new ContactId() { id = registrantId} };
            NameServers = nameservers;
        }
    }
}
