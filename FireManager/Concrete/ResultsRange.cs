﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class ResultsRanges
    {        
        [XmlElement(ElementName = "range")]
        public ResultRange[] Range { get; set; }
    }
}
