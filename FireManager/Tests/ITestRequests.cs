using FireManager.Entities.MemberAggregate;
using FireManager.Entities.PositionAggregate;
using FireManager.Entities.ScheduleAggregate;
using FireManager.Entities.StaffedPositionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Tests
{
    public interface ITestRequests
    {
        Task<bool> RunTestSuite();

        Task<IList<FireManagerMember>> TestMemberRequest();
        Task<IList<FireManagerSchedule>> TestScheduleRequest();
        Task<IList<FireManagerPosition>> TestPositionRequest();
        Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(DateTime Date);
        Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(int Year, int Month);
    }
}
