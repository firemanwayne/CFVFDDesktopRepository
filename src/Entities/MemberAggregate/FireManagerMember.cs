using FireManager.Concrete;
using System.Linq;

namespace FireManager.Entities
{
    public class FireManagerMember
    {
        public FireManagerMember() { }

        FireManagerMember(Member Member)
        {
            Name = Member.Name.Value ?? "Unknown";
            MemberId = Member.Id.ToString();
            Email = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(9)).Value?.value ?? "Unknown";
            PhoneNumber = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(9)).Value?.value ?? "Unknown";
            EmployeeTypeId = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(34)).Id.ToString();
            EmployeeType = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(34)).Value?.value ?? "Unknown";
            HireDate = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(5)).Value?.value ?? "Unknown";
            Rank = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(53)).Value?.value ?? "Unknown";
            Station = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(45)).Value?.value ?? "Unknown";
            PrNumber = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(PRNumberAttributeId)).Value?.value ?? "Unknown";
            Status = Member.Attributes.Attribute.FirstOrDefault(a => a.Id.Equals(104)).Value?.value;
        }

        public string MemberId { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string EmployeeTypeId { get; }
        public string EmployeeType { get; }
        public string HireDate { get; }
        public string Rank { get; }
        public string Station { get; }
        public string PrNumber { get; }
        public string Status { get; }

        private const int PRNumberAttributeId = 46;

        public static implicit operator FireManagerMember(Member e) => new(e);
    }
}
