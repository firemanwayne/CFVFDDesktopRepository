using System.Collections.Generic;

namespace FireManager.Concrete
{
    public class Element
    {
        public string TagName { get; set; }
        public string ElementValue { get; set; }
        public int ElementId { get; set; }
        public IList<ElementAttributes> ListElementAttributes { get; set; }
    }
}