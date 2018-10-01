using System;
using System.Collections.Generic;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Shared.DomainClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class DailyWorkout : Entity, ITimestampable
    {
        private ISet<Workout> _workouts = new HashSet<Workout>();
        private ISet<Slot> _slots = new HashSet<Slot>();
        public Day Day { get; protected  set; }
        public DateTime? UpdatedAt { get;protected set; }
        public DateTime CreatedAt { get;protected set; }
        
        public IEnumerable<Slot> Slots
        {
            get => _slots;
            protected set => _slots = new HashSet<Slot>(value);
        }

        public IEnumerable<Workout> Workouts
        {
            get => _workouts;
            protected set => _workouts = new HashSet<Workout>(value);
        }
    }
}