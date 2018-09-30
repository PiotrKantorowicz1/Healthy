using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutCategory : ValueObject<WorkoutCategory>
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        protected WorkoutCategory()
        {
        }

        public WorkoutCategory(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Workout category id can not be empty.", nameof(id));
            }

            if (name.Empty())
            {
                throw new ArgumentException("Workout category name can not be empty.", nameof(name));
            }

            Id = id;
            Name = name;
        }
        
        public static WorkoutCategory Create(Guid id, string name)
            => new WorkoutCategory(id, name);

        public static WorkoutCategory Create(string name)
            => WorkoutCategory.Create(Guid.NewGuid(), name);       

        protected override bool EqualsCore(WorkoutCategory other) => Id.Equals(other.Id);

        protected override int GetHashCodeCore() => Id.GetHashCode();
    }
}