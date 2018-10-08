using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public static class WorkoutSubType
    {
        public const string Split = "Split";
        public const string FBW = "Full Body Workout";

        public static bool IsValid(string workoutSubType)
        {
            if (string.IsNullOrWhiteSpace(workoutSubType))
            {
                return false;
            }

            workoutSubType = workoutSubType.ToLowerInvariant();

            return workoutSubType == Split || workoutSubType == FBW;
        }
    }
}