using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Shared.DomainClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class Workout : Entity, ITimestampable
    {
        private ISet<WorkoutGroup> _workoutGroups = new HashSet<WorkoutGroup>();
        public string Name { get; protected set; }
        public Interval Duration { get; protected set; }
        public string WorkoutType { get; protected set; }
        public string WorkoutSubType { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public IEnumerable<WorkoutGroup> WorkoutGroups
        {
            get => _workoutGroups;
            protected set => _workoutGroups = new HashSet<WorkoutGroup>(value);
        }

        protected Workout()
        {
        }

        public Workout(Guid id, string name, Interval duration, string workoutType,
            string workoutSubType)
        {
            Id = Id;
            SetName(name);
            SetDuration(duration);
            WorkoutType = workoutType;
            WorkoutSubType = workoutSubType;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
            {
                throw new DomainException(ErrorCodes.NameNotProvided,
                    "Workout name cannot be empty.");
            }

            if (name.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidName,
                    "Workout name is too long.");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDuration(Interval duration)
        {
            if (duration == null)
            {
                throw new DomainException(ErrorCodes.DurationNotProvided,
                    "Duration can not be null.");
            }

            Duration = duration;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddWorkoutItem(WorkoutGroup workoutGroup)
        {
            _workoutGroups.Add(new WorkoutGroup(workoutGroup.Id, workoutGroup.Name));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveWorkoutGroup(Guid id)
        {
            var workoutGroup = GetWorkoutGroupOrFail(id);
            _workoutGroups.Remove(workoutGroup);
            UpdatedAt = DateTime.UtcNow;
        }

        public WorkoutGroup GetWorkoutGroupOrFail(Guid id)
        {
            var workoutGroup = GetWorkoutGroup(id);
            if (workoutGroup.HasNoValue)
            {
                throw new DomainException(ErrorCodes.WorkoutGroupNotFound,
                    $"Workout group with id: '{id}' was not found.");
            }

            return workoutGroup.Value;
        }

        private Maybe<WorkoutGroup> GetWorkoutGroup(Guid id)
            => WorkoutGroups.SingleOrDefault(x => x.Id == id);
    }
}