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
