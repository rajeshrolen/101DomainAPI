using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI.Domain
{
    [XmlRoot("ZoneInfo")]
    public class ZoneInfo
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";

        [XmlElement("Zone")]
        public string Zone;

        public ZoneInfo()
        {
        }

        public ZoneInfo(string zone)
        {
            Zone = zone;
        }
    }
}
