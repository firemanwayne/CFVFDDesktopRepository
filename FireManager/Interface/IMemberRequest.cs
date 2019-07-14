using FireManager.Entities.MemberAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IMemberRequest
    {
        Task<Stream> StreamMembersAsync(bool ActiveOnly);
        Task<IList<FireManagerMember>> GetMembersAsync(bool IsActive);
    }
}
