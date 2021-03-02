namespace FireManager.Concrete
{
    public class ElementAttributes
    {
        public string ElementType { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string ElementValue { get; set; }

        public static MemberElementAttributeTypes GetElementAttributeFromId(int id)
        {
            foreach (MemberElementAttributeTypes attribute in MemberElementAttributeTypes.ListElementAttributeTypes)
                if (attribute.Id.Equals(id))
                    return attribute;

            return new MemberElementAttributeTypes();
        }

        public static MemberElementAttributeTypes GetElementAttributeFromKey(string Key)
        {
            foreach (MemberElementAttributeTypes attribute in MemberElementAttributeTypes.ListElementAttributeTypes)
                if (attribute.Key.Equals(Key))
                    return attribute;

            return new MemberElementAttributeTypes();
        }
    }
}