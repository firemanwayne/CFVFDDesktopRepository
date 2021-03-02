using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class TimeType
    {
        [XmlText]
        public string Value { get; set; }
    }
}
