using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Authentication
    {       
        [XmlAttribute(AttributeName = "code")]
        public int Code { get; set; }

        [XmlElement(ElementName = "member")]
        public Member Member { get; set; }
    }
}
