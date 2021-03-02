using System.Xml.Serialization;

namespace FireManager.Concrete
{
    public class Error
    {
        [XmlAttribute(AttributeName = "code")]
        public int Code { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
