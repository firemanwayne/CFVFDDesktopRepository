using FireManager.Concrete;
using System;

namespace FireManager.Entities
{
    public class FireManagerStaffedPosition
    {
        private FireManagerStaffedPosition() { }
        private FireManagerStaffedPosition(Schedule Schedule, Position Position, Member Member, DateTime Begin, DateTime End)
        {
            if (Schedule == null)
                throw new ArgumentNullException(nameof(Schedule));

            if (Position == null)
                throw new ArgumentNullException(nameof(Position));

            if (Member == null)
                throw new ArgumentNullException(nameof(Member));

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name?.Value;
            PositionId = Position.Id;
            PositionName = Position.Name?.Value;
            MemberId = Member.Id;
            MemberName = Member.Name?.Value;
            StartShift = Begin;
            EndShift = End;
        }

        public static FireManagerStaffedPosition Instance(Schedule schedule, Position position, Member member, DateTime begin, DateTime end)
        {
            return new FireManagerStaffedPosition(schedule, position, member, begin, end);
        }

        public int ScheduleId { get; }
        public string ScheduleName { get; }
        public int PositionId { get; }
        public string PositionName { get; }
        public int MemberId { get; }
        public string MemberName { get; }
        public DateTime StartShift { get; }
        public DateTime EndShift { get; }
        public TimeSpan TotalHours { get => EndShift - StartShift; }
    }
}
