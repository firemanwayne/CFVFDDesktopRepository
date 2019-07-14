using FireManager.Concrete;
using FireManager.Entities.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Entities.PositionAggregate
{
    public class FireManagerPosition
    {
        private FireManagerPosition() { }
        private FireManagerPosition(Schedule Schedule, Position Position)
        {
            if (Schedule == null)
                throw new ArgumentNullException("Schedule cannot be null");

            if (Position == null)
                throw new ArgumentNullException("Position cannot be null");
            
            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name.Value;

            Id = Position.Id;
            Name = Position.Name.Value;
        }
        private FireManagerPosition(Schedule Schedule, int PositionId, string PositionName)
        {
            if (Schedule == null)
                throw new ArgumentNullException("Schedule cannot be null");

            if (PositionId == 0)
                throw new ArgumentException("Position Id cannot be 0");

            if (string.IsNullOrEmpty(PositionName))
                throw new ArgumentNullException("Position Name cannot be null or empty");
            
            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name.Value;
            this.Id = PositionId;
            this.Name = PositionName;
        }
        private FireManagerPosition(FireManagerSchedule Schedule, int PositionId, string PositionName)
        {
            if (Schedule == null)
                throw new ArgumentNullException("Schedule cannot be null");

            if (PositionId == 0)
                throw new ArgumentException("Position Id cannot be 0");

            if (string.IsNullOrEmpty(PositionName))
                throw new ArgumentNullException("Position Name cannot be null or empty");

            ScheduleId = Schedule.Id;
            ScheduleName = Schedule.Name;
            this.Id = PositionId;
            this.Name = PositionName;
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
            this.Id = PositionId;
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
