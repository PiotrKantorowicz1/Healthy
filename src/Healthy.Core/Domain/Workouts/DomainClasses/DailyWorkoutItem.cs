using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class DailyWorkoutItem : Entity
    {
        public string Name { get; set; }
        public string ExerciseName { get; set; }
        public int Repeats { get; set; }
        public int Series { get; set; }
    }
}