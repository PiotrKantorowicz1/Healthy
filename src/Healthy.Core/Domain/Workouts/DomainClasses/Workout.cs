using System;
using System.Collections.Generic;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class Workout : Entity, ITimestampable
    {
        private ISet<WorkoutItem> _workoutItems = new HashSet<WorkoutItem>();
        public string Name { get; protected set; }
        public string WorkoutType { get; protected set; }
        public string WorkoutSubType { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        
        public IEnumerable<WorkoutItem> WorkoutItems
        {
            get => _workoutItems;
            protected set => _workoutItems = new HashSet<WorkoutItem>(value);
        }
    }
}