using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Member
    {       
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public Name Name { get; set; }

        [XmlElement(ElementName = "attributes")]
        public Attributes Attributes { get; set; }
    }
}
