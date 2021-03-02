using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Value
    {
        [XmlText]
        public string value { get; set; }
    }
}
