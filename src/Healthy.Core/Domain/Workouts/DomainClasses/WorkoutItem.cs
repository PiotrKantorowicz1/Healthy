using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutItem 
    {
        public Guid WorkoutId { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public string ExerciseName { get; protected set; }
        public ExerciseDetails ExerciseDetails { get; protected set; }
        public string BodyGroup { get; protected set; }

        protected WorkoutItem()
        {           
        }

        public WorkoutItem(Guid workoutId, Guid exerciseId, string exerciseName,
            ExerciseDetails exerciseDetails, string bodyGroup)
        {
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            SetExerciseName(exerciseName);
            SetExerciseDetails(exerciseDetails);
            BodyGroup = bodyGroup;
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
        }

        public void SetExerciseDetails(ExerciseDetails exerciseDetails)
        {
            if (exerciseDetails == null)
            {
                throw new DomainException(ErrorCodes.ExerciseDetailsNotProvided,
                    "Exercise details can not be null");
            }

            ExerciseDetails = exerciseDetails;
        }
    }
}