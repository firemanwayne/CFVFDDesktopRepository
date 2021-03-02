using FireManager.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IPositionRequest
    {
        Task<Stream> StreamPositionsAsync();
        IAsyncEnumerable<FireManagerPosition> GetPositionsAsync();
    }
}
