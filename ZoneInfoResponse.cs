using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _101DomainsAPI
{
    [XmlRoot("ZoneInfo", Namespace = "https://api.101domain.com")]
    public sealed class ZoneInfoResponse
    {
        public sealed class LangText
        {
            [XmlAttribute("lang")]
            public string lang;

            [XmlText()]
            public string Name;

            public LangText()
            {
            }
        }

        public sealed class Amount
        {
            [XmlAttribute("currency")]
            public string Currency;

            [XmlText()]
            public decimal Value;

            public Amount()
            {
            }
        }

        public sealed class PricingType
        {
            [XmlElement("Duration")]
            public int Duration;

            [XmlElement("Amount")]
            public Amount Amount;

            public PricingType()
            {
            }
        }

        [XmlElement("Zone")]
        public ZoneListResponse.ZoneInfo Zone;

        [XmlElement("CountryName")]
        public string CountryName;

        [XmlElement("CountryISO")]
        public string CountryISO;

        [XmlElement("Requirements")]
        public string Requirements;

        [XmlElement("Preferred")]
        public bool Preferred;

        [XmlElement("SetupFee")]
        public decimal SetupFee;

        [XmlElement("CreateDate")]
        public DateTime CreateDate;

        [XmlElement("TTR")]
        public string TTR;

        [XmlElement("RP")]
        public string RP;

        [XmlElement("RGP")]
        public string RGP;

        [XmlElement("LengthMin")]
        public int LengthMin;

        [XmlElement("LengthMax")]
        public int LengthMax;

        [XmlElement("Transferable")]
        public bool Transferable;

        [XmlElement("TrusteeProductId")]
        public int TrusteeProductId;

        [XmlElement("Pricing")]
        public PricingType Pricing;

        [XmlArrayItem("CountryName")]
        public LangText[] CountryNameI18n;

        [XmlArrayItem("CountryDemonym")]
        public LangText[] CountryDemonymI18n;

        [XmlArrayItem("Requirement")]
        public LangText[] RequirementsI18n; 

        [XmlArrayItem("Pricing")]
        public PricingType[] AllPricing;

        [XmlArrayItem("Pricing")]
        public PricingType[] AllSetup;

        [XmlArrayItem("TTL")]
        public LangText[] TTLI18n;

        [XmlElement("IDN")]
        public LangText IDN;

        [XmlArrayItem("Field")]
        public DomainForRegister.Field[] Fields;

        [XmlElement("IDNServiceFee")]
        public PricingType IDNServiceFee;


        public ZoneInfoResponse()
        {
        }
    }
}
