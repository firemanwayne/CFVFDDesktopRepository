using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Member
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "name")]
        public Name Name { get; set; }

        [XmlElement(ElementName = "attributes")]
        public Attributes Attributes { get; set; }
    }
}
