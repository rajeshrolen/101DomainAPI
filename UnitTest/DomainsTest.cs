//===============================================
//this lib is developed by Dr. Rajesh Kumar Rolen
//===============================================

using _101DomainsAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;

namespace UnitTest
{
    /// <summary>
    ///This is a test class for DomainsTest and is intended
    ///to contain all DomainsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DomainsTest
    {
        static Domains target;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string url = "https://api-ote.101domain.com/do";
            string login = "101";
            string password = "xpqkbksbrz";
            target = new Domains(url, login, password);
        }
        #endregion

        /// <summary>
        ///A test for CreateContact
        ///</summary>
        [TestMethod()]
        public void ContactsTest()
        {
            // first create contact
            Contact contact = new Contact("Rajesh", "Rolen", "Microlen Inc", "dr.rajeshrolen@gmail.com", "delhi", "delhi", "India",
                                           new Contact.PhoneType() { cc = "7", ext = "", phone = "9764584199" });
            string id = target.CreateContact(contact);

            // get contact by id and check
            Contact contactResult = target.GetContact(id);
            compareContacts(contact, contactResult);

            // update contact
            contact.FirstName = "Vlad";
            target.UpdateContact(id, contact);

            // get it again and check it was pdated
            contactResult = target.GetContact(id);
            compareContacts(contact, contactResult);

            // now delete
            target.DeleteContact(id);

            // check that it is deleted
            try
            {
                contactResult = target.GetContact(id);
                Assert.Fail("Call to GetContact should be failed");
            }
            catch (Exception ex)
            {
                // expected exception!
            }
        }

        private static void compareContacts(Contact contact, Contact contactResult)
        {
            Assert.AreEqual(contact.Address1, contactResult.Address1);
            Assert.AreEqual(contact.City, contactResult.City);
            Assert.AreEqual(contact.Company, contactResult.Company);
            Assert.AreEqual(contact.Country, contactResult.Country);
            Assert.AreEqual(contact.Email, contactResult.Email);
            Assert.AreEqual(contact.FirstName, contactResult.FirstName);
            Assert.AreEqual(contact.LastName, contactResult.LastName);
            Assert.AreEqual(contact.Phone.cc, contactResult.Phone.cc);
            Assert.AreEqual(contact.Phone.ext, contactResult.Phone.ext);
            Assert.AreEqual(contact.Phone.phone, contactResult.Phone.phone);
        }

        /// <summary>
        ///A test for CheckDomains
        ///</summary>
        [TestMethod()]
        public void CheckDomainsTest()
        {
            string notAvailDomain = "microsoft.com";
            string availDomain = "ahanarolen1504.com";
            DomainForCheck[] domains = new DomainForCheck[] { new DomainForCheck(notAvailDomain), new DomainForCheck(availDomain) };
            CheckResponse.CheckedDomain[] result = target.CheckDomains(domains);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(result[0].Name.ToLower(), notAvailDomain);
            Assert.IsFalse(result[0].Avail);
            Assert.AreEqual(result[1].Name.ToLower(), availDomain);
            Assert.IsTrue(result[1].Avail);
        }

        /// <summary>
        ///A test for RegisterDomain
        ///</summary>
        [TestMethod()]
        public void RegisterDomainTest()
        {
            string domainName = string.Format("ahanarolen{0}.com", (new Random()).Next(Int32.MaxValue));

            // check it is available
            DomainForCheck[] domains = new DomainForCheck[] { new DomainForCheck(domainName) };
            CheckResponse.CheckedDomain[] result = target.CheckDomains(domains);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(result[0].Name.ToLower(), domainName);
            Assert.IsTrue(result[0].Avail);

            // we need to create contact first for domain registration
            Contact contact = new Contact("Rajesh", "Rolen", "Microlen Inc", "dr.rajeshrolen@gmail.com", "delhi", "delhi", "India",
                                           new Contact.PhoneType() { cc = "7", ext = "", phone = "9764584199" });
            string id = target.CreateContact(contact);

            // register domain
            DomainForRegister domain = new DomainForRegister(domainName, 12, false, id, new string[] { "NS1.101DOMAIN.COM", "NS2.101DOMAIN.COM" });
            domain.Contacts.Admin = new ContactId() { id = id };
            domain.UserAccount = new DomainForRegister.UserAccountType() { create = false, Email = "dr.rajeshrolen@gmail.com" };
            domain.Fields = new DomainForRegister.Field[] { new DomainForRegister.Field("org-type", "org") };
            RegisterResponse response = target.RegisterDomain(domain);

            // check it was registered fine
            Assert.AreEqual(response.Domain.ToLower(), domainName);
            Assert.AreEqual(response.Expiration, DateTime.MinValue);
            Assert.AreEqual(response.Contacts.Admin, id);
            Assert.AreEqual(response.Contacts.Billing, id);
            Assert.AreEqual(response.Contacts.Registrant, id);
            Assert.AreEqual(response.Contacts.Technical, id);
            Assert.AreEqual(response.NameServers.Length, domain.NameServers.Length);
            for (int i = 0; i < response.NameServers.Length; ++i)
            {
                Assert.AreEqual(response.NameServers[i], domain.NameServers[i]);
            }
            Assert.AreEqual(response.StatusFlags.Length, 1);
            Assert.AreEqual(response.StatusFlags[0], "pendingCreate");

            DomainForCheck domainInfo = new DomainForCheck(domainName);
            InfoResponse infoResponse = target.InfoDomain(domainInfo);

            // check info is still the same
            Assert.AreEqual(infoResponse.Domain.ToLower(), domainName);
            Assert.AreEqual(infoResponse.Expiration, DateTime.MinValue);
            Assert.AreEqual(infoResponse.Contacts.Admin, id);
            Assert.AreEqual(infoResponse.Contacts.Billing, id);
            Assert.AreEqual(infoResponse.Contacts.Registrant, id);
            Assert.AreEqual(infoResponse.Contacts.Technical, id);
            Assert.AreEqual(response.NameServers.Length, domain.NameServers.Length);
            for (int i = 0; i < infoResponse.NameServers.Length; ++i)
            {
                Assert.AreEqual(infoResponse.NameServers[i], domain.NameServers[i]);
            }
            Assert.AreEqual(infoResponse.StatusFlags.Length, 1);
            Assert.AreEqual(infoResponse.StatusFlags[0], "pendingCreate");

            // we need to create another contact for domain update
            Contact contact2 = new Contact("Rajesh", "Rolen", "Microlen Inc", "dr.rajeshrolen@gmail.com", "delhi", "delhi", "India",
                                           new Contact.PhoneType() { cc = "7", ext = "", phone = "9764584199" });
            string id2 = target.CreateContact(contact);

            DomainForUpdate domainUpdate = new DomainForUpdate(domainName, new string[] { "NS1.101DOMAIN.COM", "NS2.101DOMAIN.COM" });
            domainUpdate.Contacts.Admin = new ContactId() { id = id2 };
            domainUpdate.Contacts.Billing = new ContactId() { id = id2 };
            domainUpdate.Contacts.Registrant = new ContactId() { id = id2 };
            domainUpdate.Contacts.Technical = new ContactId() { id = id2 };
            target.UpdateDomain(domainUpdate);

            //if no exception - it should be updated fine.
            //Delete old contact
            target.DeleteContact(id);

            domainInfo = new DomainForCheck(domainName);
            infoResponse = target.InfoDomain(domainInfo);

            // check info again
            Assert.AreEqual(infoResponse.Domain.ToLower(), domainName);
            Assert.AreEqual(infoResponse.Expiration, DateTime.MinValue);
            Assert.AreEqual(infoResponse.Contacts.Admin, id2);
            Assert.AreEqual(infoResponse.Contacts.Billing, id2);
            Assert.AreEqual(infoResponse.Contacts.Registrant, id2);
            Assert.AreEqual(infoResponse.Contacts.Technical, id2);
            Assert.AreEqual(response.NameServers.Length, domain.NameServers.Length);
            for (int i = 0; i < infoResponse.NameServers.Length; ++i)
            {
                Assert.AreEqual(infoResponse.NameServers[i], domain.NameServers[i]);
            }
            Assert.AreEqual(infoResponse.StatusFlags.Length, 1);
            Assert.AreEqual(infoResponse.StatusFlags[0], "pendingCreate");

            try
            {
                DomainForRenew domainRenew = new DomainForRenew(domainName, 12);
                target.RenewDomain(domainRenew);
                Assert.Fail("Domain can't be renewed");
            }
            catch (Exception ex)
            {
                //Expected exception
                Assert.AreEqual("Object status prohibits operation", ex.Message);
            }
        }

        /// <summary>
        ///A test for Zones & Product
        ///</summary>
        [TestMethod()]
        public void ZonesTest()
        {
            ZoneListResponse.ZoneInfo[] result = target.ZoneList();
            Assert.IsTrue(result.Length > 0); // We can only know answer should be without error and some zones should be returned
            target.ZoneInfo(result[0].Name);
        }

        /// <summary>
        ///A test for checkError
        ///</summary>
        [TestMethod()]
        public void checkErrorTest()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<DOMAPI xmlns=\"https://api.101domain.com\"><Response><ContactCreate errorCode=\"1000\" errorMessage=\"Command completed successfully\" requestId=\"1\"><id>7F24C</id> </ContactCreate></Response></DOMAPI>");
            target.checkError(doc);
            string id = target.extractSingle(doc, "id");
            Assert.AreEqual("7F24C", id);

            doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><DOMAPI xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"https://api.101domain.com api.xsd\" xmlns=\"https://api.101domain.com\" testmode=\"1\"><Response><ContactInfo errorCode=\"1000\" errorMessage=\"Command completed successfully\"><id>7F24C</id><Contact><FirstName>rajesh</FirstName><LastName>rolen</LastName><Company>microlent Inc</Company><Email>dr.rajeshrolen@gmail.com</Email><Address1>delhi</Address1><City>delhi</City><State></State><Postal></Postal><Country>India</Country><Phone cc=\"7\" ext=\"\">9764584199</Phone><Fax cc=\"1\" ext=\"\"></Fax></Contact></ContactInfo></Response></DOMAPI>");
            target.checkError(doc);
            ContactResponse contact = target.extractObject<ContactResponse>(doc);
        }
    }
}
