using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    public sealed class ContactId
    {
        [XmlElement("id")]
        public string id;
    }

    [XmlRoot( "ContactCreate")]
    public class Contact
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";

        public sealed class PhoneType
        {
            [XmlAttribute("cc")]
            public string cc;

            [XmlAttribute("ext")]
            public string ext;

            [XmlText()]
            public string phone;
        }

        [XmlElement("FirstName")]
        public string FirstName;

        [XmlElement("LastName")]
        public string LastName;

        [XmlElement("Company")]
        public string Company;

        [XmlElement("Email")]
        public string Email;

        [XmlElement("Address1")]
        public string Address1;

        [XmlElement("Address2")]
        public string Address2;

        [XmlElement("Address3")]
        public string Address3;

        [XmlElement("City")]
        public string City;

        [XmlElement("State")]
        public string State;

        [XmlElement("Postal")]
        public string Postal;

        [XmlElement("Country")]
        public string Country;

        [XmlElement("Phone")]
        public PhoneType Phone;

        [XmlElement("Fax")]
        public PhoneType Fax;

        public Contact()
        {
        }

        public Contact(string firstName, string lastName, string company, string email, string address1, string city, string country, PhoneType phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Email = email;
            Address1 = address1;
            City = city;
            Country = country;
            Phone = phone;
        }
    }
}
