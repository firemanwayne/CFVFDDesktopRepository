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
