using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Name
    {
        [XmlText]
        public string Value { get; set; }
    }
}
