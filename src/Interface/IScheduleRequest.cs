using FireManager.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FireManager.Interface
{
    public interface IScheduleRequest
    {
        Task<Stream> StreamSchedulesAsync();
        IAsyncEnumerable<FireManagerSchedule> GetSchedulesAsync();
    }
}
