using FireManager.Entities.MemberAggregate;
using FireManager.Entities.PositionAggregate;
using FireManager.Entities.ScheduleAggregate;
using FireManager.Entities.StaffedPositionAggregate;
using FireManager.Extensions;
using FireManager.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Tests
{
    public class TestRequests : ITestRequests
    {
        private readonly FireManagerOptions Options;
        private readonly IMemberRequest MemberRequest;
        private readonly IScheduleRequest ScheduleRequest;
        private readonly IPositionRequest PositionRequest;
        private readonly IStaffedPositionRequest StaffedPositionRequest;

        public TestRequests(
            IMemberRequest MemberRequest,
            IScheduleRequest ScheduleRequest,
            IPositionRequest PositionRequest,
            IOptions<FireManagerOptions> Options,
            IStaffedPositionRequest StaffedPositionRequest)
        {
            this.Options = Options.Value;
            this.MemberRequest = MemberRequest;
            this.ScheduleRequest = ScheduleRequest;
            this.PositionRequest = PositionRequest;
            this.StaffedPositionRequest = StaffedPositionRequest;
        }

        public async Task<IList<FireManagerMember>> TestMemberRequest()
        {
            var Results = await MemberRequest.GetMembersAsync(false);
            return Results;
        }
        public async Task<IList<FireManagerSchedule>> TestScheduleRequest()
        {
            var Results = await ScheduleRequest.GetSchedulesAsync();
            return Results;
        }
        public async Task<IList<FireManagerPosition>> TestPositionRequest()
        {
            var Results = await PositionRequest.GetPositionsAsync();
            return Results;
        }
        public async Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(DateTime Date)
        {
            var Results = await StaffedPositionRequest.GetStaffedPositionsAsync(Date);
            return Results;
        }
        public async Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(int Year, int Month)
        {
            var Results = await StaffedPositionRequest.GetStaffedPositionsAsync(Month, Year);
            return Results;
        }

        public async Task<bool> RunTestSuite()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Testing Members");
                var MemberResults = await TestMemberRequest();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Members returned {MemberResults.Count()} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Testing Schedules");

                var ScheduleResult = await TestScheduleRequest();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Schedules returned {ScheduleResult.Count()} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Testing Positions");

                var PositionResult = await TestPositionRequest();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Positions returned {PositionResult.Count()} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Testing Staffed Positions for {DateTime.Today}");

                var StaffedPositionTodayResult = await TestStaffedPositionRequest(DateTime.Today);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Staffed Position by Date returned {StaffedPositionTodayResult.Count()} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Testing Staffed Positions for Year: {DateTime.Today.Year} Month: {DateTime.Today.AddMonths(-1).Month}");

                var StaffedPositionYearMonthResult = await TestStaffedPositionRequest(DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Staffed Positions by Year/Month returned {StaffedPositionYearMonthResult.Count()} records");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
