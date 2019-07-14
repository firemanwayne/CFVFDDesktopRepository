using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Attribute
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }

        [XmlElement(ElementName = "value")]
        public Value Value { get; set; }
    }
}
