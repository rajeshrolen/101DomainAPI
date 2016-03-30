using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ZoneList", Namespace = "https://api.101domain.com")]
    public sealed class ZoneListResponse
    {
        public sealed class ZoneInfo
        {
            [XmlAttribute("active")]
            public bool Active;

            [XmlText()]
            public string Name;
        }

        [XmlElement("Zone")]
        public ZoneInfo[] Zones;

        public ZoneListResponse()
        {
        }
    }
}
