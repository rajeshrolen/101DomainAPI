using System.Xml.Serialization;

namespace _101DomainsAPI
{
    /// <summary>
    /// Container for domains checks\info retrieve
    /// </summary>
    public sealed class DomainForCheck
    {
        [XmlText()]
        public string DomainName;

        [XmlAttribute("idn")]
        public string IDN;

        public DomainForCheck()
        {
            DomainName = null;
            IDN = null;
        }

        /// <summary>
        /// Check for usual domain
        /// </summary>
        /// <param name="name">domain name for check</param>
        public DomainForCheck(string name)
        {
            DomainName = name;
            IDN = null;
        }

        /// <summary>
        /// Check for national domain
        /// </summary>
        /// <param name="name">domain name for check</param>
        /// <param name="idn">idn of language of domain (can be retrieved with ZoneInfo)</param>
        public DomainForCheck(string name, string idn)
        {
            DomainName = name;
            IDN = idn;
        }
    }
}
