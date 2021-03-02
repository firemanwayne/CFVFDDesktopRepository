using System;

namespace FireManager.Concrete
{
    public class Range
    {
        public Element Schedule { get; set; }
        public Element Member { get; set; }
        public Element Position { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}