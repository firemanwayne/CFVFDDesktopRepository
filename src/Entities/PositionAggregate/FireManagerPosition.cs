using FireManager.Concrete;
using System;

namespace FireManager.Entities
{
    public class FireManagerPosition
    {
        private FireManagerPosition() { }
        private FireManagerPosition(Schedule Schedule, Position Position)
        {
            if (Schedule == null)
                throw new ArgumentNullException(nameof(Schedule));

            if (Position == null)
                throw new ArgumentNullException(nameof(Position));

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name.Value;

            Id = Position.Id;
            Name = Position.Name.Value;
        }
        private FireManagerPosition(Schedule Schedule, int PositionId, string PositionName)
        {
            if (Schedule == null)
                throw new ArgumentNullException(nameof(Schedule));

            if (PositionId == 0)
                throw new ArgumentException(null, nameof(PositionId));

            if (string.IsNullOrEmpty(PositionName))
                throw new ArgumentNullException(nameof(PositionName));

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name.Value;
            Id = PositionId;
            Name = PositionName;
        }
        private FireManagerPosition(FireManagerSchedule Schedule, int PositionId, string PositionName)
        {
            if (Schedule == null)
                throw new ArgumentNullException(nameof(Schedule));

            if (PositionId == 0)
                throw new ArgumentException(null, nameof(PositionId));

            if (string.IsNullOrEmpty(PositionName))
                throw new ArgumentNullException(nameof(PositionName));

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name;
            Id = PositionId;
            Name = PositionName;
        }

        public static FireManagerPosition Instance(Schedule Schedule, Position Position)
        {
            return new FireManagerPosition(Schedule, Position);
        }

        public static FireManagerPosition Instance(Schedule Schedule, int PositionId, string PositionName)
        {
            return new FireManagerPosition(Schedule, PositionId, PositionName);
        }

        public static FireManagerPosition Instance(FireManagerSchedule Schedule, int PositionId, string PositionName)
        {
            return new FireManagerPosition(Schedule, PositionId, PositionName);
        }

        public FireManagerPosition(int PositionId)
        {
            Id = PositionId;
        }
        public FireManagerPosition(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; }
        public string Name { get; }
        public int ScheduleId { get; }
        public string ScheduleName { get; }
        public DateTime BeginShift { get; }
        public DateTime EndShift { get; }
        public int OrderIndexNumber { get; }
        public Member Member { get; private set; }
    }
}
