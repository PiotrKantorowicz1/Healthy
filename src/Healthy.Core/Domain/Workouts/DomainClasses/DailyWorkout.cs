using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Shared.DomainClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class DailyWorkout : Entity, ITimestampable
    {
        private ISet<Workout> _workouts = new HashSet<Workout>();
        public Day Day { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public IEnumerable<Workout> Workouts
        {
            get => _workouts;
            protected set => _workouts = new HashSet<Workout>(value);
        }

        protected DailyWorkout()
        {
        }

        public DailyWorkout(Guid id, Day day)
        {
            Id = id;
            SetDay(day);
        }

        public void SetDay(Day day)
        {
            if (day == null)
            {
                throw new DomainException(ErrorCodes.InvalidDay,
                    "day can not be null");
            }

            Day = day;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddWorkout(Workout workout)
        {
            _workouts.Add(new Workout(workout.Id, workout.Name, workout.Duration, workout.WorkoutType,
                workout.WorkoutSubType));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveWorkout(Guid id)
        {
            var workout = GetWorkoutOrFail(id);
            _workouts.Remove(workout);
            UpdatedAt = DateTime.UtcNow;
        }

        public Workout GetWorkoutOrFail(Guid id)
        {
            var workout = GetWorkout(id);
            if (workout.HasNoValue)
            {
                throw new DomainException(ErrorCodes.WorkoutNotFound,
                    $"Workout with id: '{id}' was not found.");
            }

            return workout.Value;
        }

        private Maybe<Workout> GetWorkout(Guid id)
            => Workouts.SingleOrDefault(x => x.Id == id);
    }
}