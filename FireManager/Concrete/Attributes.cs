using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Attributes
    {
        [XmlElement(ElementName = "attribute")]
        public Attribute[] Attribute { get; set; }
    }
}
