using FireManager.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Entities.StaffedPositionAggregate
{
    public class FireManagerStaffedPosition
    {
        private FireManagerStaffedPosition() { }
        private FireManagerStaffedPosition(Schedule Schedule, Position Position, Member Member, DateTime Begin, DateTime End)
        {
            if (Schedule == null)
                throw new ArgumentNullException("Schedule cannot be null");

            if (Position == null)
                throw new ArgumentNullException("Position cannot be null");

            if (Member == null)
                throw new ArgumentNullException("Position cannot be null");

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name?.Value;
            PositionId = Position.Id;
            PositionName = Position.Name?.Value;
            MemberId = Member.Id;
            MemberName = Member.Name?.Value;
            StartShift = Begin;
            EndShift = End;
        }

        public static FireManagerStaffedPosition Instance(Schedule Schedule, Position Position, Member Member, DateTime Begin, DateTime End)
        {
            return new FireManagerStaffedPosition(Schedule, Position, Member, Begin, End);
        }

        public int ScheduleId { get; private set; }
        public string ScheduleName { get; private set; }
        public int PositionId { get; private set; }
        public string PositionName { get; private set; }
        public int MemberId { get; private set; }
        public string MemberName { get; private set; }
        public DateTime StartShift { get; private set; }
        public DateTime EndShift { get; private set; }

        public TimeSpan TotalHours { get => EndShift - StartShift; }
    }
}
