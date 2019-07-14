using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class ResultRange
    {        
        [XmlElement(ElementName = "schedule")]
        public Schedule Schedule { get; set; }

        [XmlElement(ElementName = "position")]
        public Position Position { get; set; }

        [XmlElement(ElementName = "timetype")]
        public TimeType TimeType { get; set; }

        [XmlElement(ElementName = "member")]
        public Member Member { get; set; }

        [XmlElement("begin")]
        public DateTime Begin
        {
            get { return begin; }
            set { begin = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")); }
        }

        [XmlElement("end")]
        public DateTime End
        {
            get { return end; }
            set { end = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")); }
        }

        private DateTime begin;
        private DateTime end;
    }
}
