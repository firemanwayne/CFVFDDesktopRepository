using FireManager.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IStaffedPositionRequest
    {
        Task<Stream> StreamStaffedPositionsAsync(int Month, int Year);
        Task<Stream> StreamStaffedPositionsAsync(DateTime RequestDate);
        Task<Stream> StreamStaffedPositionsAsync(DateTime StartDate, DateTime EndDate);

        Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(int Month, int Year);
        Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime RequestDate);
        Task<IList<FireManagerStaffedPosition>> GetStaffedPositionsAsync(DateTime StartDate, DateTime EndDate);
    }
}
