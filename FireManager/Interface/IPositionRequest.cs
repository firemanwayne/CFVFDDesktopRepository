using FireManager.Entities.PositionAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IPositionRequest
    {
        Task<Stream> StreamPositionsAsync();
        Task<IList<FireManagerPosition>> GetPositionsAsync();
    }
}
