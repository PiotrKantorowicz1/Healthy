using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutItem : Entity, ITimestampable
    {
        public Guid WorkoutId { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public string ExerciseName { get; protected set; }
        public ExerciseDetails ExerciseDetails { get; protected set; }
        public string BodyGroup { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected WorkoutItem()
        {           
        }

        public WorkoutItem(Guid id, Guid workoutId, Guid exerciseId, string exerciseName,
            ExerciseDetails exerciseDetails, string bodyGroup)
        {
            Id = id;
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            SetExerciseName(exerciseName);
            SetExerciseDetails(exerciseDetails);
            BodyGroup = bodyGroup;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetExerciseName(string exerciseName)
        {
            if (exerciseName.Empty())
            {
                throw new DomainException(ErrorCodes.NameNotProvided,
                    "Workout item name cannot be empty.");
            }

            if (exerciseName.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidName,
                    "Workout item name is too long.");
            }

            ExerciseName = exerciseName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetExerciseDetails(ExerciseDetails exerciseDetails)
        {
            if (exerciseDetails == null)
            {
                throw new DomainException(ErrorCodes.ExerciseDetailsNotProvided,
                    "Exercise details can not be null");
            }

            ExerciseDetails = exerciseDetails;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}