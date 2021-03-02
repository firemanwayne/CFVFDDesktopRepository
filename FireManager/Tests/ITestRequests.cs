using FireManager.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireManager.Tests
{
    public interface ITestRequests
    {
        /// <summary>
        /// Calls all test methods and returns whether the test completed or not
        /// </summary>
        /// <returns>bool value indicating all tests were completed</returns>
        Task<bool> RunTestSuite();

        /// <summary>
        /// Test method for Members
        /// </summary>
        /// <returns>List of FireManager Members</returns>
        IAsyncEnumerable<FireManagerMember> TestMemberRequest();

        /// <summary>
        /// Test method for Schedules
        /// </summary>
        /// <returns>List of FireManager Schedules</returns>
        IAsyncEnumerable<FireManagerSchedule> TestScheduleRequest();

        /// <summary>
        /// Test method for Positions
        /// </summary>
        /// <returns>List of FireManager Positions</returns>
        IAsyncEnumerable<FireManagerPosition> TestPositionRequest();

        /// <summary>
        /// Test method for Staffed Positions
        /// </summary>
        /// <param name="Date">Specified date of staffed schedules</param>
        /// <returns>List of Staffed Positions for the specified date</returns>
        Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(DateTime Date);

        /// <summary>
        /// Test method for Staffed Positions
        /// </summary>
        /// <param name="Year">Specified year of staffed schedules</param>
        /// <param name="Month">Specified month of staffed schedules</param>
        /// <returns>List of Staffed Positions for the specified year and month</returns>
        Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(int Year, int Month);
    }
}
