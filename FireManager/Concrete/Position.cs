using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Position
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public Name Name { get; set; }
    }
}