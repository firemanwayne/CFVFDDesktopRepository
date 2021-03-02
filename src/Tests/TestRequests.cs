using FireManager.Entities;
using FireManager.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireManager.Tests
{
    public class TestRequests : ITestRequests
    {
        private readonly IMemberRequest MemberRequest;
        private readonly IScheduleRequest ScheduleRequest;
        private readonly IPositionRequest PositionRequest;
        private readonly IStaffedPositionRequest StaffedPositionRequest;

        public TestRequests(
            IMemberRequest MemberRequest,
            IScheduleRequest ScheduleRequest,
            IPositionRequest PositionRequest,
            IStaffedPositionRequest StaffedPositionRequest)
        {
            this.MemberRequest = MemberRequest;
            this.ScheduleRequest = ScheduleRequest;
            this.PositionRequest = PositionRequest;
            this.StaffedPositionRequest = StaffedPositionRequest;
        }

        public IAsyncEnumerable<FireManagerMember> TestMemberRequest() => MemberRequest.GetMembersAsync(false);
        public IAsyncEnumerable<FireManagerSchedule> TestScheduleRequest() => ScheduleRequest.GetSchedulesAsync();
        public IAsyncEnumerable<FireManagerPosition> TestPositionRequest() => PositionRequest.GetPositionsAsync();

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

                var members = new List<FireManagerMember>();
                await foreach (var m in TestMemberRequest())
                    members.Add(m);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Members returned {members.Count} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Testing Schedules");

                var schedules = new List<FireManagerSchedule>();

                await foreach (var s in TestScheduleRequest())
                    schedules.Add(s);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Schedules returned {schedules.Count} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Testing Positions");

                var positions = new List<FireManagerPosition>();
                await foreach (var p in TestPositionRequest())
                    positions.Add(p);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Positions returned {positions.Count} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Testing Staffed Positions for {DateTime.Today}");

                var StaffedPositionTodayResult = await TestStaffedPositionRequest(DateTime.Today);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Staffed Position by Date returned {StaffedPositionTodayResult.Count} records");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Testing Staffed Positions for Year: {DateTime.Today.Year} Month: {DateTime.Today.AddMonths(-1).Month}");

                var StaffedPositionYearMonthResult = await TestStaffedPositionRequest(DateTime.Today.Year, DateTime.Today.AddMonths(-1).Month);

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Staffed Positions by Year/Month returned {StaffedPositionYearMonthResult.Count} records");

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
