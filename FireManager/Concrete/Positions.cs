using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Positions
    {        
        [XmlElement(ElementName = "position")]
        public Position[] Position { get; set; }
    }
}