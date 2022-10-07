using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcoStruxureConfigurator.XML
{
    public class XML_classes
    {
        public class ObjectSet
        {
            [XmlAttribute]
            public string ExportMode;
            [XmlAttribute]
            public string Note;
            [XmlAttribute]
            public string Version;
            public XML_MetaInformation MetaInformation;
            public List<OI> ExportedObjects;
        }

        public class XML_MetaInformation
        {
            public XML_TagValueString ExportMode { get; set; }
            public XML_TagValueString RuntimeVersion { get; set; }
            public XML_TagValueString SourceVersion { get; set; }
            public XML_TagValueString ServerFullPath { get; set; }
        }

        public class XML_TagValueString
        {
            [XmlAttribute]
            public string Value { get; set; }
        }

        [XmlRoot("OI")]
        public class OI
        {
            [XmlAttribute]
            public string NAME;
            [XmlAttribute]
            public string DESCR;
            [XmlAttribute]
            public string TYPE;

            [XmlElement("PI")]
            public List<PI> PIList;

            [XmlElement("OI")]
            public List<OI> OIList;

            public OI()
            {
            }

            public OI(string name, string type, string descr = null)
            {
                NAME = name;
                TYPE = type;
                DESCR = descr;
            }
        }

        [XmlRoot("Reference")]
        public class Reference
        {
            [XmlAttribute]
            public string DeltaFilter;
            [XmlAttribute]
            public string Locked;
            [XmlAttribute]
            public string Object;
            [XmlAttribute]
            public string Property;
            [XmlAttribute]
            public string Retransmit;
            [XmlAttribute]
            public string TransferRate;

            public Reference()
            {
            }

            public Reference(string obj, string deltaFilter = null, string locked = null, string retransmit = null, string transferRate = null, string property = null)
            {
                DeltaFilter = deltaFilter;
                Locked = locked;
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj));
                }
                else
                {
                    //Object = "~/System/Content Types/" + obj;
                    Object = obj;
                }
                Property = property;
                Retransmit = retransmit;
                TransferRate = transferRate;
            }
        }

        [XmlRoot("PI")]
        public class PI
        {
            [XmlAttribute]
            public string Name;
            [XmlAttribute]
            public string Value;

            [XmlElement("Reference")]
            public Reference reference;


            public PI()
            {
            }

            public PI(string name, string value = null)
            {
                Name = name;
                Value = value;
            }

        }

    }
}
