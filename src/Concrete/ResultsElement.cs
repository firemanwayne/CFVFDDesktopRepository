namespace FireManager.Concrete
{
    public class ResultsElement : Element
    {
        public string Version { get; set; }
        public string AccessTime { get; set; }
        private new string ElementValue { get; set; }
    }
}