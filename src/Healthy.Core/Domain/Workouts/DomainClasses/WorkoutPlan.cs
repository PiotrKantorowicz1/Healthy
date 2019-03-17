using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Shared.ValueObjects;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutPlan : AggregateRoot, ITimestampable
    {
        private ISet<DailyWorkout> _dailyWorkout = new HashSet<DailyWorkout>();
        public string UserId { get; protected set; }
        public Interval Interval { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        
        public IEnumerable<DailyWorkout> DailyWorkouts
        {
            get => _dailyWorkout;
            protected set => _dailyWorkout = new HashSet<DailyWorkout>(value);
        }
        
        protected WorkoutPlan()
        {
        }

        public WorkoutPlan(Guid id, string userId, Interval interval)
        {
            Id = id;
            UserId = userId;
            Interval = interval;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }
        
        public void SetInterval(Interval interval)
        {
            Interval = interval ?? throw new DomainException(ErrorCodes.IntervalNotProvided,
                    "Interval can not be null.");
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddDailyWorkout(DailyWorkout dailyWorkout)
        {
            _dailyWorkout.Add(new DailyWorkout(dailyWorkout.Id, dailyWorkout.Day));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveDailyWorkout(Guid id)
        {
            var dailyWorkout = GetDailyWorkoutOrFail(id);
            _dailyWorkout.Remove(dailyWorkout);
            UpdatedAt = DateTime.UtcNow;
        }

        public DailyWorkout GetDailyWorkoutOrFail(Guid id)
        {
            var dailyWorkout = GetDailyWorkout(id);
            if (dailyWorkout.HasNoValue)
            {
                throw new DomainException(ErrorCodes.DailyWorkoutNotFound,
                    $"Daily workout with id: '{id}' was not found.");
            }

            return dailyWorkout.Value;
        }

        private Maybe<DailyWorkout> GetDailyWorkout(Guid id)
            => DailyWorkouts.SingleOrDefault(x => x.Id == id);
    }
}