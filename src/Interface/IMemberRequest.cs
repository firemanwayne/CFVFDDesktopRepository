using FireManager.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IMemberRequest
    {
        Task<Stream> StreamMembersAsync(bool ActiveOnly);
        IAsyncEnumerable<FireManagerMember> GetMembersAsync(bool IsActive);
    }
}
