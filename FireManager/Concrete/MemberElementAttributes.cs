using System.Collections.Generic;

namespace FireManager.Concrete
{
    public class MemberElementAttributeTypes
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ElementTypeName { get; set; }
        public string ElementValue { get; set; }

        public readonly static List<MemberElementAttributeTypes> ListElementAttributeTypes = new()
        {
            new MemberElementAttributeTypes() { Id = 1, Key = "first_name", ElementTypeName = "First Name" },
            new MemberElementAttributeTypes() { Id = 3, Key = "last_name", ElementTypeName = "Last Name" },
            new MemberElementAttributeTypes() { Id = 5, Key = "hire_date", ElementTypeName = "Hire Date" },
            new MemberElementAttributeTypes() { Id = 7, Key = "mobile_phone", ElementTypeName = "Mobile Phone" },
            new MemberElementAttributeTypes() { Id = 9, Key = "email", ElementTypeName = "Email" },
            new MemberElementAttributeTypes() { Id = 34, Key = "employee_type", ElementTypeName = "Employee Type" },
            new MemberElementAttributeTypes() { Id = 45, ElementTypeName = "Station" },
            new MemberElementAttributeTypes() { Id = 46, ElementTypeName = "PR Number" },
            new MemberElementAttributeTypes() { Id = 53, ElementTypeName = "Rank" },
            new MemberElementAttributeTypes() { Id = 104, ElementTypeName = "Status" }
        };
    }
}