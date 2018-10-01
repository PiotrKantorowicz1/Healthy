using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutItem : Entity, ITimestampable
    {
        public Guid WorkoutId { get; protected set; }
        public string Name { get; protected set; }
        public string ExerciseName { get; protected set; }
        public int Repeats { get; protected set; }
        public int Series { get; protected set; }
        public int Break { get; protected set; }
        public string BodyGroup { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}