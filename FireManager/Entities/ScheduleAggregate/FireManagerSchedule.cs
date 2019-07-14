﻿using FireManager.Concrete;
using FireManager.Entities.PositionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManager.Entities.ScheduleAggregate
{
    public class FireManagerSchedule
    {
        private Schedule schedule;

        private FireManagerSchedule() { }
        private FireManagerSchedule(Schedule Schedule)
        {
            schedule = Schedule ?? throw new ArgumentNullException("Schedule object cannot be null");

            Id = Schedule.Id;
            Name = Schedule.Name.Value;

            if (Schedule.Positions != null)
                foreach (var Position in schedule.Positions.ToList())
                    foreach (var item in Position.Position.ToList())
                        AddPosition(item);
        }
        private FireManagerSchedule(int Id, string Name)
        {
            if (Id == 0)
                throw new ArgumentException("Schedule Id cannot be 0");

            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("Schedule name cannot be null or empty");

            this.Id = Id;
            this.Name = Name;
        }

        public static FireManagerSchedule Instance(Schedule Schedule)
        {
            return new FireManagerSchedule(Schedule);
        }
        public static FireManagerSchedule Instance(int Id, string Name)
        {
            return new FireManagerSchedule(Id, Name);
        }

        public int Id { get; }
        public string Name { get; }
        public int PositionCount { get; private set; }
        public IList<FireManagerPosition> Positions { get; private set; } = new List<FireManagerPosition>();

        private void AddPosition(Position Position)
        {
            if (schedule != null)
            {
                Positions.Add(FireManagerPosition.Instance(schedule, Position));
                PositionCount++;
            }
        }
    }
}
