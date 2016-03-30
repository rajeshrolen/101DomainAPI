using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ContactUpdate")]
    public class ContactForUpdate : Contact
    {
        [XmlElement("id")]
        public string id;

        public ContactForUpdate()
        {
        }

        public ContactForUpdate(string id, Contact contact)
        {
            this.Address1 = contact.Address1;
            this.Address2 = contact.Address2;
            this.Address3 = contact.Address3;
            this.City = contact.City;
            this.Company = contact.Company;
            this.Country = contact.Country;
            this.Email = contact.Email;
            this.Fax = contact.Fax;
            this.FirstName = contact.FirstName;
            this.id = id;
            this.LastName = contact.LastName;
            this.Phone = contact.Phone;
            this.Postal = contact.Postal;
            this.State = contact.State;
        }
    }
}
