using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutGroup : AggregateRoot, ITimestampable
    {
        private ISet<WorkoutItem> _workoutItems = new HashSet<WorkoutItem>();
        public string Name { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<WorkoutItem> WorkoutItems
        {
            get => _workoutItems;
            protected set => _workoutItems = new HashSet<WorkoutItem>(value);
        }

        protected WorkoutGroup()
        {
        }

        public WorkoutGroup(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            if (name.Empty())
            {
                throw new DomainException(ErrorCodes.NameNotProvided,
                    "Workout group name cannot be empty.");
            }

            if (name.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidName,
                    "Workout group name is too long.");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddWorkoutItem(WorkoutItem workoutItem)
        {
            _workoutItems.Add(new WorkoutItem(workoutItem.WorkoutId, workoutItem.ExerciseId,
                workoutItem.ExerciseName, workoutItem.ExerciseDetails, workoutItem.BodyGroup));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveWorkoutItem(Guid id)
        {
            var workoutItem = GetWorkoutItemOrFail(id);
            _workoutItems.Remove(workoutItem);
            UpdatedAt = DateTime.UtcNow;
        }

        public WorkoutItem GetWorkoutItemOrFail(Guid id)
        {
            var workoutItem = GetWorkoutItem(id);
            if (workoutItem.HasNoValue)
            {
                throw new DomainException(ErrorCodes.WorkoutItemNotFound,
                    $"Workout item with id: '{id}' was not found.");
            }

            return workoutItem.Value;
        }

        private Maybe<WorkoutItem> GetWorkoutItem(Guid exerciseId)
            => WorkoutItems.SingleOrDefault(x => x.ExerciseId == exerciseId);
    }
}