using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI.Domain
{
    [XmlRoot("ZoneList")]
    public class ZoneList
    {
        [XmlAttribute("requestId")]
        public string requestId = "1";

        public ZoneList()
        {
        }
    }
}
